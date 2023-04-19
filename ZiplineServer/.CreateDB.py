###################################################################################################
# .CreateDB.py
###################################################################################################
# Create empty database with users and files tables if one does not exist.
###################################################################################################

import sqlite3

database = 'Zipline.db'

sqlTablesExist = '''
    SELECT      name
    FROM        sqlite_master
    WHERE       name='USERS' or name='FILES' or name='ACCESS';'''

sqlCreateUsersTable = '''
    CREATE TABLE "USERS"("USER_ID"          INTEGER NOT NULL UNIQUE,
                         "USERNAME"         TEXT NOT NULL UNIQUE,
                         "PW_HASH"          TEXT,
                         "ONLINESTATUS"     INTEGER NOT NULL,
                         "LATEST_IP"        TEXT NOT NULL,
                         PRIMARY KEY("USER_ID")
    );'''

sqlCreateFilesTable = '''
    CREATE TABLE "FILES" ("FILE_ID"         INTEGER NOT NULL UNIQUE,
                          "USER_ID"         INTEGER NOT NULL,
                          "FILE_GUID"       TEXT NOT NULL UNIQUE,
                          "FILENAME"        TEXT NOT NULL,
                          "FILESIZE"        NUMERIC NOT NULL,
                          PRIMARY KEY("FILE_ID" AUTOINCREMENT),
                          FOREIGN KEY("USER_ID") REFERENCES "USERS"("USER_ID")
    );'''

sqlCreateAccessTable = '''
    CREATE TABLE "ACCESS" ("ACCESS_ID"      INTEGER NOT NULL UNIQUE,
                           "FILE_GUID"      TEXT NOT NULL,
                           "USER_ID"        INTEGER NOT NULL,
                           PRIMARY KEY("ACCESS_ID" AUTOINCREMENT),
                           FOREIGN KEY("FILE_GUID") REFERENCES "FILES"("FILE_GUID")
                           FOREIGN KEY("USER_ID") REFERENCES "USERS"("USER_ID")
    );'''


dbConnection = sqlite3.connect(database)
dbCursor = dbConnection.cursor()

result = dbCursor.execute(sqlTablesExist)
if (result.fetchone() is None):
    print("Database Not Found; Creating Empty Database")
    dbCursor.execute(sqlCreateUsersTable)
    dbCursor.execute(sqlCreateFilesTable)
    dbCursor.execute(sqlCreateAccessTable)

dbConnection.commit()
dbConnection.close()
