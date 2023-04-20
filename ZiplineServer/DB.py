###################################################################################################
# DB.py
###################################################################################################
# Module containing database functions used by the server.
###################################################################################################

import SQL
import sqlite3


####
# database: filename of server database
####
database = 'Zipline.db'


####
# addNewFile(payload: json_object)
####
def addNewFile(payload: 'json_object with Command, Username, FileGUID, Filename, FileSize, AuthorizedUsers Fields'):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    username = payload['Username']
    fileguid = payload['FileGUID']
    filename = payload['Filename']
    filesize = payload['FileSize']
    authusers = payload['AuthorizedUsers'].split('?')

    filexists = cursor.execute(SQL.queryFile, [fileguid]).fetchone()
    if filexists:
        return 'STATUS_FILE_EXISTS'
    
    cursor.execute(SQL.insertFile, [fileguid, filename, filesize, username])
    cursor.execute(SQL.insertAccess, [fileguid, username])

    connection.commit()
    connection.close()
    return 'STATUS_OK'

####
# deleteFile(payload: json_object)
####
def deleteFile(payload: 'json_object with Command, FileGUID Fields'):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    fileguid = payload['FileGUID']
    cursor.execute(SQL.deleteAccess, [fileguid])
    cursor.execute(SQL.deleteFile, [fileguid])

    connection.commit()
    connection.close()
    return 'STATUS_OK'


####
# loginUser(payload: json_object)
####
def loginUser(payload: 'json_object with Command, Username, LatestIP Fields'):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    username = payload['Username']
    password = payload['Password']
    latestip = payload['LatestIP']
    
    result = cursor.execute(SQL.queryUser, [username]).fetchone()
    if not result:
        cursor.execute(SQL.insertOnlineUser, [username, password, latestip])
    else:
        cursor.execute(SQL.updateUserOnline, [latestip, username])

    connection.commit()
    connection.close()
    return 'STATUS_OK'


####
# logoutUser(payload: json_object)
####
def logoutUser(payload: 'json_object with Command, Username Fields'):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    username = payload['Username']

    result = cursor.execute(SQL.queryUser, [username]).fetchone()
    if not result:
        return 'STATUS_IGNORE'
    else:
        cursor.execute(SQL.updateUserOffline, [username])

    connection.commit()
    connection.close()
    return 'STATUS_OK'

####
# getUsersAndFiles()
####
def getUsersAndFiles():
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    result = cursor.execute(SQL.queryUsersAndFiles).fetchall()

    connection.commit()
    connection.close()
    return result


####
# getUserIP(payload: json_object)
####
def getUserIP(payload: 'json_object with Command, TargetUser Fields'):
    connection = sqlite3.connect(database)
    cursor = connection.cursor()

    username = payload['TargetUser']
    result = cursor.execute(SQL.queryUserIP, [username]).fetchone()
    if not result:
        result = 'STATUS_USER_OFFLINE'

    connection.commit()
    connection.close()
    return result

####
# verifyUserFiles(payload: json_object)
####
def verifyUserFiles(payload: 'json_object with Command, Username, FileList Fields'):
    connection = sqlite3.connect(database)
    cursor =  connection.cursor()

    username = payload['Username']
    filelist = payload['FileList']

    clientFiles = filelist.split('?')
    serverFiles = [x[0] for x in cursor.execute(SQL.queryUserFiles, [username]).fetchall()]
    allFiles = list(dict.fromkeys(clientFiles + serverFiles))

    result = []
    for file in allFiles:
        if file in clientFiles and file in serverFiles:
            status = 'STATUS_OK'
        elif file in clientFiles and file not in serverFiles:
            status = 'STATUS_UNKNOWN_FILE'
        elif file in serverFiles and file not in clientFiles:
            status = 'STATUS_MISSING_FILE'
        else:
            status = 'STATUS_LOGIC_ERROR'
        result.append({file : status})

    connection.commit()
    connection.close()
    return result
