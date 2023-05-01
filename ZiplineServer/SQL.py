###################################################################################################
# SQL.py
###################################################################################################
# Module containing SQL statements used by the server.
###################################################################################################

#####
# Statement used to check if a database file contains the proper schema.
#####
checkTablesExist = '''
    SELECT      name
    FROM        sqlite_master
    WHERE       name='USERS' or name='FILES' or name='ACCESS';
'''


#####
# Statements to create tables within a newly created database file.
#####
createUsersTable = '''
    CREATE TABLE "USERS" ("USER_ID"         INTEGER NOT NULL UNIQUE,
                          "USERNAME"        TEXT NOT NULL UNIQUE,
                          "PW_HASH"         TEXT,
                          "ONLINESTATUS"    INTEGER NOT NULL,
                          "LATEST_IP"       TEXT NOT NULL,
                          PRIMARY KEY("USER_ID"));
'''

createFilesTable = '''
    CREATE TABLE "FILES" ("FILE_ID"         INTEGER NOT NULL UNIQUE,
                          "USER_ID"         INTEGER NOT NULL,
                          "FILE_GUID"       TEXT NOT NULL UNIQUE,
                          "FILENAME"        TEXT NOT NULL,
                          "FILESIZE"        NUMERIC NOT NULL,
                          PRIMARY KEY("FILE_ID" AUTOINCREMENT),
                          FOREIGN KEY("USER_ID") REFERENCES "USERS"("USER_ID"));
'''

createAccessTable = '''
    CREATE TABLE "ACCESS" ("ACCESS_ID"      INTEGER NOT NULL UNIQUE,
                           "FILE_GUID"      TEXT NOT NULL,
                           "USER_ID"        INTEGER NOT NULL,
                           PRIMARY KEY("ACCESS_ID" AUTOINCREMENT),
                           FOREIGN KEY("FILE_GUID") REFERENCES "FILES"("FILE_GUID")
                           FOREIGN KEY("USER_ID")   REFERENCES "USERS"("USER_ID"));
'''


#####
# Statements to delete entries from tables.
#####
deleteAccess = '''
    DELETE FROM     ACCESS
    WHERE           FILE_GUID = ?;
'''

deleteFile = '''
    DELETE FROM     FILES
    WHERE           FILE_GUID = ?;
'''


#####
# Statements to extract data from tables.
#####
queryFile = '''
    SELECT          FILENAME
    FROM            FILES
    WHERE           FILES.FILE_GUID = ?;
'''

queryFileGUID = '''
    SELECT          FILES.FILE_GUID
    FROM            FILES
    INNER JOIN      USERS ON FILES.USER_ID = USERS.USER_ID
    WHERE           USERS.USERNAME = ? and FILES.FILENAME = ?;
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


#####
# Statements to insert entries into tables.
#####
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

#####
# Statements to update existing entries within tables.
#####
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


###################################################################################################
# EOF
###################################################################################################
