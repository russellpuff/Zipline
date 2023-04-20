#######################################################################################################################
## .TestClient.py
#######################################################################################################################
## A simple CLI client to quickly test server features.
#######################################################################################################################

import socket

host, port  = '137.184.241.2', 52525
headerBytes = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])


#######################################################################################################################
## Supporting Functions
#######################################################################################################################

####
# sendPackage(payload)
####
# Support function to construct a package, create a socket, send the package across it, and return the response
####
def sendPackage(payload):
    size = (4 + len(headerBytes) + len(bytes(payload, 'utf-8'))).to_bytes(4, 'big')
    package = size + headerBytes + bytes(payload, 'utf-8')
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((host, port))
    sock.sendall(package)
    response = str(sock.recv(1024))
    sock.close()
    return response


####
# genResponse(package)
####
# Support function to generate the server's response to a byte sequence.
####
def genResponse(package):
    size = (4 + len(bytes(package, 'utf-8'))).to_bytes(4, 'big')
    return str(size + bytes(package, 'utf-8'))


####
# testResults(testname, response, expected)
####
# Support function to print the results of an individual test; returns 1 if test fails, 0 otherwise.
####
def testResults(testname, response, expected):
    if response == expected:
        print("[ ] " + testname)
        return 0
    else:
        print("[X] " + testname)
        print("    RESPONSE: {}\n    EXPECTED: {}".format(response,expected))
        return 1


####
# printheader(testname)
####
# Support function to print a test header.
####
def printHeader(testname):
    header = "TESTING [" + testname + "]"
    print(header.ljust(120, '-'))
    print()


####
# printFinalResults(testname, failures)
####
# Support function to print a summary of test results.
def printFinalResults(testname, failures):
    print()
    testname = "[" + testname + "]: "
    if failures == 0:
        print(testname + "PASSES")
    else:
        print(testname + "FAILED {} TESTS".format(failures))
    print(120 * '-')



#######################################################################################################################
## Test Functions
#######################################################################################################################

####
# testAddNewFile()
####
# Test the add_new_file command.
####
def testAddNewFile():
    printHeader('addNewFile')
    failures = 0

    ## Test with Existing User, New File
    response = sendPackage('{"Command":"add_new_file","Username":"barbara","FileGUID":"1234-2345-3456-4567","Filename":"test.txt","FileSize":100,"AuthorizedUsers":"danielle?evan"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing User, New File Test', response, expected)

    ## Test with Existing User, Duplicate FIle
    response = sendPackage('{"Command":"add_new_file","Username":"barbara","FileGUID":"11AA-22BB-33CC-44DD","Filename":"sample.db","FileSize":64000,"AuthorizedUsers":"danielle?evan"}')
    expected = genResponse('STATUS_FILE_EXISTS')
    failures += testResults('Existing User, Duplicate File Test', response, expected)

    ## Test with Non-Existent User, New File
    response = sendPackage('{"Command":"add_new_file","Username":"nonexistentuser1","FileGUID":"1234-2345-3456-4567","Filename":"test.txt","FileSize":100,"AuthorizedUsers":"danielle?evan"}')
    expected = genResponse('STATUS_USER_DOES_NOT_EXIST')
    failures += testResults('Non-Existent User, New File Test', response, expected)

    ## Test with Non-Existent user, Duplicate File
    response = sendPackage('{"Command":"add_new_file","Username":"nonexistentuser1","FileGUID":"11AA-22BB-33CC-44DD","Filename":"sample.db","FileSize":64000,"AuthorizedUsers":"danielle?evan"}')
    expected = genResponse('STATUS_USER_DOES_NOT_EXIST')
    failures += testResults('Non-Existent User, Duplicate File Test', response, expected)

    printFinalResults('addNewFile', failures)


####
# testDeleteFile()
####
# Test the delete_file command.
####
def testDeleteFile():
    printHeader('deleteFile')
    failures = 0

    ## Test with Existing File by GUID
    response = sendPackage('{"Command":"delete_file","FileGUID":"1234-2345-3456-4567"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing File by GUID Test', response, expected)

    ## Test with Non-Existent File by GUID
    response = sendPackage('{"Command":"delete_file","FileGUID":"9999-9999-9999-9999"}')
    expected = genResponse('STATUS_IGNORE')
    failures += testResults('Non-Existent File by GUID Test', response, expected)

    ## Test with Existing File by Username, Filename
    response = sendPackage('{"Command":"delete_file","Username":"barbara","Filename":"test2.txt"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing File by Username,Filename Test', response, expected)

    ## Test with Non-Existent File by Username, Filename
    response = sendPackage('{"Command":"delete_file","Username":"barbara","Filename":"test2.txt"}')
    expected = genResponse('STATUS_IGNORE')
    failures += testResults('Non-Existent File by Username,Filename Test', response, expected)

    printFinalResults('deleteFile', failures)


####
# testLoginUser()
####
# Test the login_user command.
####
def testLoginUser():
    printHeader('loginUser')
    failures = 0

    ## Test with New User
    response = sendPackage('{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"11.22.33.44:55123"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('New User Test', response, expected)

    ## Test with Existing User, Different IP
    response = sendPackage('{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"55.66.77.88:55789"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing User Test', response, expected)

    printFinalResults('loginUser', failures)
    

####
# testLogoutUser()
####
# Test the logout_user command.
####
def testLogoutUser():
    printHeader('logoutUser')
    failures = 0

    ## Test with Existing Online User
    response = sendPackage('{"Command":"logout_user", "Username":"testuser1"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing Online User Test', response, expected)

    ## Test with Existing Offline User
    response = sendPackage('{"Command":"logout_user", "Username":"evan"}')
    expected = genResponse('STATUS_OK')
    failures += testResults('Existing Offline User Test', response, expected)

    ## Testing with Non-Existent User
    response = sendPackage('{"Command":"logout_user", "Username":"nonexistentuser1"}')
    expected = genResponse('STATUS_IGNORE')
    failures += testResults('Non-Existent User Test', response, expected)

    printFinalResults('logoutUser', failures)


####
# testGetUserIP()
####
# Test the get_user_ip command.
####
def testGetUserIP():
    printHeader('getUserIP')
    failures = 0

    ## Test with Existing Online User
    response = sendPackage('{"Command":"get_user_ip", "TargetUser":"danielle"}')
    expected = genResponse('''('{"LatestIP":"45.45.45.45:57123"}',)''')
    failures += testResults('Existing Online User Test', response, expected)

    ## Test with Existing Offline User
    response = sendPackage('{"Command":"get_user_ip", "TargetUser":"evan"}')
    expected = genResponse('STATUS_USER_OFFLINE')
    failures += testResults('Existing Offline User Test', response, expected)

    ## Test with Non-Existent User
    response = sendPackage('{"Command":"get_user_ip", "TargetUser":"nonexistentuser1"}')
    expected = genResponse('STATUS_USER_OFFLINE')
    failures += testResults('Non-Existent User Test', response, expected)

    printFinalResults('getUserIP', failures)


####
# testVerifyUserFiles()
####
# Test the verify_user_files command.
####
def testVerifyUserFiles():
    printHeader('verifyUserFiles')
    failures = 0

    ## Test One Unknown File
    response = sendPackage('{"Command":"verify_user_files","Username":"barbara","FileList":"garbage.jnk"}')
    expected = genResponse("[{'garbage.jnk': 'STATUS_UNKNOWN_FILE'}, {'mona-lisa.jpg': 'STATUS_MISSING_FILE'}, {'sample.db': 'STATUS_MISSING_FILE'}]")
    failures += testResults('Unknown File Test', response, expected)

    ## Test One Missing File
    response = sendPackage('{"Command":"verify_user_files","Username":"barbara","FileList":"sample.db"}')
    expected = genResponse("[{'sample.db': 'STATUS_OK'}, {'mona-lisa.jpg': 'STATUS_MISSING_FILE'}]")
    failures += testResults('Missing File Test', response, expected)

    ## Test All OK Files
    response = sendPackage('{"Command":"verify_user_files","Username":"barbara","FileList":"mona-lisa.jpg?sample.db"}')
    expected = genResponse("[{'mona-lisa.jpg': 'STATUS_OK'}, {'sample.db': 'STATUS_OK'}]")
    failures += testResults('All OK Files', response, expected)

    ## Test with One Unknown, One Missing, One OK
    response = sendPackage('{"Command":"verify_user_files", "Username":"barbara", "FileList":"garbage.jnk?mona-lisa.jpg"}')
    expected = genResponse("[{'garbage.jnk': 'STATUS_UNKNOWN_FILE'}, {'mona-lisa.jpg': 'STATUS_OK'}, {'sample.db': 'STATUS_MISSING_FILE'}]")
    failures += testResults('"One of Each" Test', response, expected)

    printFinalResults('verifyUserFiles', failures)


#######################################################################################################################
# Perform Tests
#######################################################################################################################

testAddNewFile()
testDeleteFile()
testLoginUser()
testLogoutUser()
testGetUserIP()
testVerifyUserFiles()
