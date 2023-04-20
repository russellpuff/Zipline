###################################################################################################
# .FillDB.py
###################################################################################################
# Populate the database with dummy data for testing
###################################################################################################

import sqlite3

database = 'Zipline.db'

userData = [
        (10022, 'albert',   'das$31lf0',   1, '55.111.2.63:55555'),
        (10015, 'barbara',  '@98se492',    1, '201.104.103.58:52526'),
        (10038, 'charlie',  'ASlkdas7',    1, '199.7.4.98:57321'),
        (10098, 'danielle', 'al82l1mAd',   1, '45.45.45.45:57123'),
        (10001, 'evan',     '98123lkMAS1', 0, '1.2.3.4:12345')
]

fileData = [
        (99032, 10022, 'AA93-AB34-7718-BB2C', 'shrek.mkv', 4192000000),
        (99618, 10015, 'AA11-HG7T-YR33-3L3R', 'mona-lisa.jpg', 768000),
        (99005, 10038, 'ABCD-EFGH-IJKL-MNOP', 'boot.tar', 500),
        (99101, 10098, '1234-ABCD-1234-ABCD', 'data.dat', 110056085),
        (99889, 10001, 'ZZZZ-YYYY-XXXX-WWWW', 'plain.txt', 12002),
        (99890, 10015, '11AA-22BB-33CC-44DD', 'sample.db', 64000)
]

accessData = [
        (55001, 'AA93-AB34-7718-BB2C', 10022),
        (55002, 'AA11-HG7T-YR33-3L3R', 10022),
        (55003, 'AA93-AB34-7718-BB2C', 10015),
        (55004, 'ZZZZ-YYYY-XXXX-WWWW', 10015),
        (55005, 'AA93-AB34-7718-BB2C', 10098)
]

dbConnection = sqlite3.connect(database)
dbCursor = dbConnection.cursor()

dbCursor.executemany("INSERT INTO 'USERS' VALUES(?, ?, ?, ?, ?)", userData)
dbCursor.executemany("INSERT INTO 'FILES' VALUES(?, ?, ?, ?, ?)", fileData)
dbCursor.executemany("INSERT INTO 'ACCESS' VALUES(?, ?, ?)", accessData)

dbConnection.commit()
dbConnection.close()
