########################################################################################################################
# DB.py
########################################################################################################################
# - Module containing functions interfacing between the server and the database.
########################################################################################################################

import base64
import os
import re
import socket
import sqlite3

import Log
import SQL
import TCP


#####
# database: string
#####
# - filename of the server database
#####
DATABASE = '.ZiplineDatabase.db'


########################################################################################################################
# SQLite Wrapper Class with Context Management
########################################################################################################################

class SQLite():
    def __enter__(self):
        self.connection = sqlite3.connect(DATABASE)
        return self.connection.cursor()
    def __exit__(self, type, value, traceback):
        self.connection.commit()
        self.connection.close()


########################################################################################################################
# Helper Functions
########################################################################################################################

#####
# checkPayload(payload: JSON Object, fields: list of string)
#####
# - Returns True if all fields are present in the payload.
# - Returns False if they are not.
#####
def checkPayload(payload, fields):
    for field in fields:
        if field not in payload.keys():
            Log.error('Malformed Package Payload; Expected the Following Fields: {}'.format(fields))
            return False
    return True


#####
# parseIPAddress(text: string)
#####
# - Expects a string containing an ip address that may be in IPv4 or IPv6 with or without a port
# - Returns the address portion of the string
#####
def parseIPAddress(text):
    ip = text[12:].strip('"}')
    print('Text: {}'.format(text))
    print('IP: {}'.format(ip))
    return text[12:].strip('"}')


########################################################################################################################
# Database Functions
########################################################################################################################

#####
# addNewFile(payload: JSON Object)
#####
# - Expects payload to have Command, Username, FileGUID, Filename, FileSize, and AuthorizedUsers Fields.
#####
def addNewFile(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','FileGUID','Filename','FileSize','AuthorizedUsers'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']
        fileguid = payload['FileGUID']
        filename = payload['Filename']
        filesize = payload['FileSize']
        authuser = payload['AuthorizedUsers'].split('?')

        ## Check that User exists
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            return 'STATUS_USER_DOES_NOT_EXIST'

        ## Check that File does not exist
        fileexists = database.execute(SQL.queryFile, [fileguid]).fetchone()
        if fileexists:
            return 'STATUS_FILE_EXISTS'

        ## Add File and Access records to the database
        database.execute(SQL.insertFile, [fileguid, filename, filesize, username])
        database.execute(SQL.insertAccess, [fileguid, username])
        return 'STATUS_OK'



#####
# deleteFile(payload: JSON Object)
#####
# - Expects payload to have Command and FileGUID Fields.
#####
def deleteFile(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        ## ! Files can be deleted by their GUID or by the Username and Filename
        validWithGUID = 'FileGUID' in payload.keys()
        validWithNames = 'Username' in payload.keys() and 'Filename' in payload.keys()

        ## Read FileGUID directly if possible
        if validWithGUID:
            fileguid = payload['FileGUID']
        ## Otherwise, use Username and Filename to query it
        elif validWithNames:
            username = payload['Username']
            filename = payload['Filename']
            fileguid = database.execute(SQL.queryFileGUID, [username, filename]).fetchone()
            if not fileguid:
                return 'STATUS_IGNORE'
            else:
                fileguid = fileguid[0] # fetchone() returns a tuple
        else:
            Log.error('Malformed Package Payload; Expected the Following Fields: {}  OR  {}'.format(['Command','FileGUID'],['Command','Username','Filename']))
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Check that the file exists
        fileexists = database.execute(SQL.queryFile, [fileguid]).fetchone()
        if not fileexists:
            return 'STATUS_IGNORE'

        # Delete File and Access records
        database.execute(SQL.deleteAccess, [fileguid])
        database.execute(SQL.deleteFile, [fileguid])
        return 'STATUS_OK'


#####
# downloadFile(payload: JSON Object)
#####
# - Expects payload to have Command, Username, TargetUser, and TargetGUID Fields.
#####
def downloadFile(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','TargetUser','TargetGUID'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Read payload field values
        username = payload['Username']
        target = payload['TargetUser']
        fileguid = payload['TargetGUID']

        ## Check that target user exists
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            return 'STATUS_TARGET_USER_DOES_NOT_EXIST'

        ## Check that user is online
        useronline = database.execute(SQL.queryUserIP, [target]).fetchone()
        if not useronline:
            return 'STATUS_TARGET_USER_OFFLINE'

        ## Check that file exists
        fileexists = database.execute(SQL.queryFile, [fileguid]).fetchone()
        if not fileexists:
            return 'STATUS_FILE_DOES_NOT_EXIST'

        ## Construct Payload
        payload = bytes(str(payload), 'utf-8')
        length = (4 + len(TCP.HEADERBYTES) + len(payload)).to_bytes(4, 'big')
        package = length + TCP.HEADERBYTES + payload

        ## Send Package to Target User
        targetip = parseIPAddress(useronline[0])
        targetsock = TCP.CONNECTIONS[targetip]
        Log.printResponse(package)
        TCP.send(targetsock, package)

        ## Send Response Status to Requester
        return 'STATUS_OK'
            


#####
# loadDatabase()
#####
# - Checks if the DATABASE file exists.
# - If it does, reports using the existing database.
# - Otherwise, creates a new database file, adds the necessary tables, commits, and closes the database.
#####
def loadDatabase():
    if not os.path.isfile(DATABASE):
        with SQLite() as database:
            Log.info('Creating New Database File: {}'.format(DATABASE))
            database.execute(SQL.createUsersTable)
            database.execute(SQL.createFilesTable)
            database.execute(SQL.createAccessTable)
    else:
        Log.info('Using Existing Database File: {}'.format(DATABASE))


#####
# loginUser(payload: JSON Object, socket: socket.socket)
#####
# - Expects payload to have Command, Username, and LatestIP Fields.
# - Expects socket to be the current active network socket for this connection.
#####
def loginUser(payload, socket):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','Password','LatestIP'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']
        password = payload['Password']
        latestip = payload['LatestIP']

        ## Check if user does not exist
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            database.execute(SQL.insertOnlineUser, [username, password, latestip])
            TCP.maintainConnection(latestip, socket)
            return 'STATUS_OK'

        ## Check if user online
        useronline = database.execute(SQL.queryUserIP, [username]).fetchone()
        if not useronline:
            database.execute(SQL.updateUserOnline, [latestip, username])
            useronline = database.execute(SQL.queryUserIP, [username]).fetchone()
            parseIPAddress(useronline[0])
            TCP.maintainConnection(latestip, socket)
            return 'STATUS_OK'

        ## If user exists and is already online
        TCP.maintainConnection(latestip, socket)
        return 'STATUS_OK'


#####
# logoutUser(payload: JSON Object)
#####
# - Expects payload to have Command and Username Fields.
#####
def logoutUser(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']

        ## Check if user does not exist
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            return 'STATUS_IGNORE'

        ## Check if user offline
        useronline = database.execute(SQL.queryUserIP, [username]).fetchone()
        if not useronline:
            return 'STATUS_IGNORE'

        ## If user exists and is online, logout
        address = useronline[0][12:].strip('"}')
        database.execute(SQL.updateUserOffline, [username])
        TCP.terminateConnection(address)
        return 'STATUS_OK'


#####
# getUsersAndFiles
#####
#
#####
def getUsersAndFiles():
    with SQLite() as database:
        result = database.execute(SQL.queryUsersAndFiles).fetchall()
        return result


#####
# getUserIP(payload: JSON Object)
#####
# - Expects payload to have Command and TargetUser Fields.
#####
def getUserIP(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command', 'TargetUser'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['TargetUser']

        ## If user does not exist, return 'STATUS_USER_OFFLINE'
        ## Otherwise, return result
        result = database.execute(SQL.queryUserIP, [username]).fetchone()
        if not result:
            return 'STATUS_USER_OFFLINE'
        return result


#####
# sendFile(payload: JSON Object)
#####
# - Expects payload to have Command, Response, TargetUser, and File fields.
#####
def sendFile(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Response'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'


        ## Get payload field values
        response = payload['Response']
        target = payload['TargetUser']

        ## Check target online
        targetonline = database.execute(SQL.queryUserIP, [target]).fetchone()
        if not targetonline:
            return 'STATUS_TARGET_USER_OFFLINE'

        ## If Response is not 'STATUS_OK', let target know
        if response != 'STATUS_OK':
            return payload

        ## Get target socket
        targetip = targetonline[0][12:].strip('"}')
        target = TCP.CONNECTIONS[targetip]

        ## Get file data and construct package
        filedata = bytes(payload['File'], 'utf-8')
        header = bytes([0x7f, 0x52, 0x8b, 0x59, 0xe9, 0xf9, 0x04, 0xc3])
        length = (4 + len(header) + len(filedata)).to_bytes(4, 'big')
        package = length + header + filedata
        TCP.send(target, package)
        return 'STATUS_OK'

        
        


#####
# validateSchema
#####
# - Returns True if the database contains the required tables.
# - Returns False otherwise.
#####
def validateSchema():
    connection = sqlite3.connect(DATABASE)
    cursor = connection.cursor()
    tablesExist = all(cursor.execute(SQL.checkTablesExist))
    connection.commit()
    connection.close()
    return tablesExist


#####
# verifyUserFiles(payload: JSON Object)
#####
# - Expects payload to have Command, Username, and FileList fields.
#####
def verifyUserFiles(payload):
    with SQLite() as database:
        ## verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','FileList'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']
        filelist = payload['FileList']

        ## Get client files from the payload field
        ## Get server files from a database query
        ## Get a list of all unique files found by combining lists and passing through a dictionary
        clientfiles = filelist.split('?')
        serverfiles = [x[0] for x in database.execute(SQL.queryUserFiles, [username]).fetchall()]
        allfiles = list(dict.fromkeys(clientfiles + serverfiles))

        ## Construct a list of key-value pairs where the key is the filename and
        ## the value is the file's status as either unknown (not on server),
        ## missing (not on client), or ok (found on both)
        result = []
        for file in allfiles:
            ok = file in clientfiles and file in serverfiles
            unknown = file in clientfiles and file not in serverfiles
            missing = file in serverfiles and file not in clientfiles
            if ok:
                status = 'STATUS_OK'
            elif unknown:
                status = 'STATUS_UNKNOWN'
            elif missing:
                status = 'STATUS_MISSING'
            else:
                status = 'STATUS_LOGIC_ERROR'
            result.append({file : status})
        return result


########################################################################################################################
# EOF
########################################################################################################################
