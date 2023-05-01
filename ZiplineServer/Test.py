########################################################################################################################
# Test.py
########################################################################################################################
#### - Supporting functions for the unit testing suite.
########################################################################################################################

import socket

import DB

HOST, PORT = 'localhost', 52525
HEADERBYTES = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])


#####
# generateResponse(package: string)
#####
# - Construct a valid server response as a JSON formatted string from a given input string
#   representing a valid payload.
# - The server does respond with 4 size bytes.
# - The server does NOT respond with header bytes.
#####
def generateResponse(payload):
    payload = bytes(payload,  'utf-8')
    size = (4 + len(payload)).to_bytes(4, 'big')
    package = str(size + payload)
    return package


#####
# sendPackage(payload: JSON formatted string)
#####
# - Constructs a package from the payload
# - Opens a network socket
# - Sends the package out over the socket
# - Returns the response.
#####
def sendPackage(payload):
    ## Construct Package
    payload = bytes(payload, 'UTF-8')
    size = (4 + len(HEADERBYTES) + len(payload)).to_bytes(4, 'big')
    package = size + HEADERBYTES + payload

    ## Open Socket and Send Package
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((HOST,PORT))
    sock.sendall(package)

    ## Close Socket and Return Response
    response = str(sock.recv(1024).strip())
    sock.close()
    return response


#####
# printHeader(name: string)
#####
# - Prints a test header.
#####
def printHeader(name):
    header = '[TESTING]--{}-'.format(name)
    print(header.ljust(120, '-'))
    print()


#####
# printFinalResults(name: string, failures: integer)
#####
# - Prints the summary of a tests results.
#####
def printFinalResults(name, failures):
    print()
    name = '[{}]'.format(name)
    if failures == 0:
        print('{} PASSES'.format(name))
    else:
        print('{} FAILED {} TESTS'.format(name, failures))
    print(120 * '-')
    print()


#####
# printResult(name: string, response: JSON formatted bytes in UTF-8, expected: JSON formatted bytes in UTF-8)
#####
# - Prints the result of an individual test.
# - Returns 1 if test fails for failure counting.
# - Returns 0 otherwise.
#####
def printResult(name, response, expected):
    if response == expected:
        print('[ ] {}'.format(name))
        return 0
    else:
        print('[X] {}'.format(name))
        print('    RESPONSE: {}\n    EXPECTED: {}'.format(response, expected))
        return 1


#####
# resetDatabase()
#####
# - Restores the database to its original state to prevent unintended test conflicts.
#####
userdata = [
        (10022, 'albert',  'sdfasd98u', 1, '1.2.3.4:55555'),
        (10015, 'barbara', 'ajsadjf9j', 1, '8.8.8.8:22222'),
        (10038, 'charlie', 'ijaj398js', 1, '22.22.33.33:11111'),
        (10098, 'daniele', '0ijijs34j', 1, '45.45.45.45:12345'),
        (10001, 'eduardo', 'jiojj2asl', 0, '4.3.2.1:54321')
]

filedata = [
        (99032, 10022, 'AAAA-BBBB-CCCC-DDDD', 'shrek.mkv', 4192000000),
        (99618, 10015, 'AA11-BB22-CC33-DD44', 'mona-lisa.jpg', 768000),
        (99005, 10038, 'WASD-WASD-WASD-WASD', 'boot.tar', 500),
        (99101, 10098, 'ABCD-EFGH-HIJK-LMNO', 'plain.txt', 12002),
        (99889, 10001, '1111-2222-3333-4444', 'sample.db', 64000),
        (99890, 10015, 'ZZZZ-YYYY-XXXX-WWWW', 'data.dat', 110056085)
]

accessdata = [
        (55001, 'AAAA-BBBB-CCCC-DDDD', 10022),
        (55002, '1111-2222-3333-4444', 10022),
        (55003, 'AA11-BB22-CC33-DD44', 10015),
        (55004, 'ABCD-EFGH-HIJK-LMNO', 10015),
        (55005, 'ZZZZ-YYYY-XXXX-WWWW', 10098)
]

def resetDatabase():
    DB.DATABASE = '.TestDatabase.db'
    with DB.SQLite() as database:
        # Delete Existing Records
        database.execute('DELETE FROM "USERS"')
        database.execute('DELETE FROM "FILES"')
        database.execute('DELETE FROM "ACCESS"')

        # Populate with Dummy Records
        database.executemany('INSERT INTO "USERS" VALUES(?, ?, ?, ?, ?)', userdata)
        database.executemany('INSERT INTO "FILES" VALUES(?, ?, ?, ?, ?)', filedata)
        database.executemany('INSERT INTO "ACCESS" VALUES(?, ?, ?)', accessdata)


########################################################################################################################
# EOF
########################################################################################################################
