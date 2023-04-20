###################################################################################################
# SQL.py
###################################################################################################
# Module containing SQL statements used by the server.
###################################################################################################

deleteAccess = '''
    DELETE FROM     ACCESS
    WHERE           FILE_GUID = ?;
'''

deleteFile = '''
    DELETE FROM     FILES
    WHERE           FILE_GUID = ?;
'''

queryFile = '''
    SELECT          FILENAME
    FROM            FILES
    WHERE           FILES.FILE_GUID = ?;
'''

queryUsersOnline = '''
    SELECT          LATEST_IP
    FROM            USERS
    WHERE           ONLINESTATUS = 1;
'''

queryUser = '''
    SELECT          *
    FROM            USERS
    WHERE           USERNAME = ?;
'''

queryUserFiles = '''
    SELECT          FILENAME
    FROM            FILES
    INNER JOIN      USERS ON FILES.USER_ID = USERS.USER_ID
    WHERE           USERS.USERNAME = ?;
'''

queryUserIP = '''
    SELECT          json_object('LatestIP', USERS.LATEST_IP)
    FROM            USERS
    WHERE           USERNAME = ? and ONLINESTATUS = 1;
'''

queryUsersAndFiles = '''
    SELECT          json_group_array(
                        json_object(
                            'FileGUID', FILES.FILE_GUID,
                            'Username', USERS.USERNAME,
                            'Filename', FILES.FILENAME,
                            'FileSize', FILES.FILESIZE
                        )
                    )
    FROM            FILES
    INNER JOIN      USERS ON FILES.USER_ID = USERS.USER_ID
    WHERE           USERS.ONLINESTATUS = 1;
'''

insertAccess = '''
    INSERT INTO     ACCESS (FILE_GUID, USER_ID)
    SELECT          ?, USERS.USER_ID
    FROM            USERS
    WHERE           USERS.USERNAME = ?;
'''

insertFile = '''
    INSERT INTO     FILES (USER_ID, FILE_GUID, FILENAME, FILESIZE)
    SELECT          USERS.USER_ID, ?, ?, ?
    FROM            USERS
    WHERE           USERS.USERNAME = ?;
'''

insertOnlineUser = '''
    INSERT INTO     USERS (USERNAME, PW_HASH, ONLINESTATUS, LATEST_IP)
    VALUES          (?, ?, 1, ?);
'''

updateUserOnline = '''
    UPDATE          USERS
    SET             LATEST_IP = ?, ONLINESTATUS = 1
    WHERE           USERNAME = ?;
'''

updateUserOffline = '''
    UPDATE          USERS
    SET             ONLINESTATUS = 0
    WHERE           USERNAME = ?;
'''

