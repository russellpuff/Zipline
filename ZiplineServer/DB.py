########################################################################################################################
# DB.py
########################################################################################################################
# - Module containing functions interfacing between the server and the database.
########################################################################################################################

import base64
import json
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
# checkPassword(stored: string, received: string)
#####
# - Compares stored password against the received password (excluding salts).
# - 36 bytes: 0-15 Salt, 16-35 Hashed Password
# - Returns True if they match.
# - Returns False otherwise.
#####
def checkPassword(stored, received):
    stored = base64.b64decode(stored)[16:]
    received = base64.b64decode(received)[16:]
    return stored == received
    


#####
# checkPasswordSalt(stored: string, received: string)
#####
# - Compares stored password salt against received password salt.
# - Returns True if they match.
# - Returns False otherwise.
#####
def checkPasswordSalt(stored, received):
    stored = base64.b64decode(stored)[0:16]
    received = base64.b64decode(received)[0:16]
    return stored == received


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
# getUsernameFromAddress(address: string)
#####
# - Accepts an ip address as a string.
# - Returns the associated IP address if the username is in the database.
# - Returns None otherwise.
#####
def getUsernameFromAddress(address):
    with SQLite() as database:
        username = database.execute(SQL.queryUsernameByIP, [address]).fetchone()
        if username:
            return username
        else:
            return None


#####
# markUserOffline(username: string)
#####
# - Updates database to set online status to 0 for given username.
# - Used for handling client crashes and unexpected disconnects.
#####
def markUserOffline(username):
    with SQLite() as database:
        database.execute(SQL.updateUserOffline, [username])


#####
# parseIPAddress(text: string)
#####
# - Expects a string containing an ip address that may be in IPv4 or IPv6 with or without a port
# - Returns the address portion of the string
#####
def parseIPAddress(text):
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
# changePassword(payload: JSON Object)
#####
# - Expects payload to have Command, Username, and Password Fields
#####
def changePassword(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','Password'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']
        password = payload['Password']

        ## Check password hash
        '''
        storedpass = database.execute(SQL.queryPasswordByUser, [username]).fetchone()[0]
        if not checkPasswordSalt(storedpass, password):
            salt = base64.b64decode(storedpass)[0:16]
            salt = base64.b64encode(salt)
            return '{"STATUS":"STATUS_BAD_SALT", "SALT":"' + salt.decode('utf-8') + '"}'
        if not checkPassword(storedpass, password):
            return '{"STATUS":"STATUS_PASSWORD_INCORRECT"}'
        '''

        ## Check that user exists
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            return 'STATUS_USER_DOES_NOT_EXIST'

        ## Update user password
        database.execute(SQL.updateUserPassword, [password, username])
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
        targetexists = database.execute(SQL.queryUser, [target]).fetchone()
        if not targetexists:
            return 'STATUS_TARGET_USER_DOES_NOT_EXIST'

        ## Check that target user is online
        targetonline = database.execute(SQL.queryUserIP, [target]).fetchone()
        if not targetonline:
            return 'STATUS_TARGET_USER_OFFLINE'

        ## Check that file exists
        fileexists = database.execute(SQL.queryFile, [fileguid]).fetchone()
        if not fileexists:
            return 'STATUS_FILE_DOES_NOT_EXIST'

        ## Check that user has access
        ##useraccess = database.execute(SQL.queryAccess, [username, fileguid]).fetchone()
        ##if not useraccess:
            ##return 'STATUS_ACCESS_DENIED'

        ## Construct Payload
        payload = bytes(str(payload), 'utf-8')
        length = (4 + len(TCP.HEADERBYTES) + len(payload)).to_bytes(4, 'big')
        package = length + TCP.HEADERBYTES + payload

        ## Send Package to Target User
        targetip = parseIPAddress(targetonline[0])
        if target not in TCP.CONNECTIONS.keys():
            Log.error('Target Online in Database; Connection Not Maintained')
            return 'STATUS_TARGET_OFFLINE'
        targetsock = TCP.CONNECTIONS[target]
        Log.printResponse(package, targetip)
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
# - Expects payload to have Command, Username Fields.
# - Expects socket to be the current active network socket for this connection.
#####
def loginUser(payload, socket):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username','Password'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'

        ## Get payload field values
        username = payload['Username']
        password = payload['Password']
        latestip = socket.getpeername()[0]

        ## Check if user does not exist
        userexists = database.execute(SQL.queryUser, [username]).fetchone()
        if not userexists:
            database.execute(SQL.insertOnlineUser, [username, password, latestip])
            TCP.maintainConnection(username, socket)
            return '{"STATUS":"STATUS_OK"}'

        ## Check password hash
        storedpass = database.execute(SQL.queryPasswordByUser, [username]).fetchone()[0]
        if not checkPasswordSalt(storedpass, password):
            salt = base64.b64decode(storedpass)[0:16]
            salt = base64.b64encode(salt)
            return '{"STATUS":"STATUS_BAD_SALT", "SALT":"' + salt.decode('utf-8') + '"}'
        if not checkPassword(storedpass, password):
            return '{"STATUS":"STATUS_PASSWORD_INCORRECT"}'

        ## Check if user not online
        useronline = database.execute(SQL.queryUserIP, [username]).fetchone()
        if not useronline:
            database.execute(SQL.updateUserOnline, [latestip, username])
            TCP.maintainConnection(username, socket)
            return '{"STATUS":"STATUS_OK"}'

        ## If user exists and is already online
        ## Terminate prior maintained socket and add current socket to maintain
        database.execute(SQL.updateUserOnline, [latestip, username])
        TCP.terminateConnection(username)
        TCP.maintainConnection(username, socket)
        return '{"STATUS":"STATUS_OK"}'


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
        TCP.terminateConnection(username)
        return ''


#####
# getUsersAndFiles
#####
#
#####
def getUsersAndFiles(payload):
    with SQLite() as database:
        ## Verify payload is well-formed
        valid = checkPayload(payload, ['Command','Username'])
        if not valid:
            return 'STATUS_MISSING_COMMAND_FIELDS'
        
        ## Get payload field value
        username = payload['Username']

        ## Return query result
        result = database.execute(SQL.queryUsersAndFiles, [username]).fetchall()
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

        ## Get Target IP Address
        target = TCP.CONNECTIONS[target]
        targetip = parseIPAddress(targetonline[0])

        ## If Response isn't 'STATUS_OK', send response to requester
        ## Else, send file to requester
        if response != 'STATUS_OK':
            ## Construct Package
            payload = json.dumps(payload).encode('utf-8')
            header = TCP.HEADERBYTES
            length = (4 + len(header) + len(payload)).to_bytes(4, 'big')
            package = length + header + payload
            ## Send Package
            Log.printResponse(package, targetip)
            TCP.send(target, package)
            ## Respond to Request
            return payload
        else:
            ## Construct Package
            payload = bytes(payload['File'], 'utf-8')
            header = bytes([0x7f, 0x52, 0x8b, 0x59, 0xe9, 0xf9, 0x04, 0xc3])
            length = (4 + len(header) + len(payload)).to_bytes(4, 'big')
            package = length + header + filedata
            ## Send Package
            Log.printResponse(package, targetip)
            TCP.send(target, package)
            ## Respond to Request
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
                status = 'STATUS_UNKNOWN_FILE'
            elif missing:
                status = 'STATUS_MISSING_FILE'
            else:
                status = 'STATUS_LOGIC_ERROR'
            result.append({file : status})
        return result


########################################################################################################################
# EOF
########################################################################################################################
