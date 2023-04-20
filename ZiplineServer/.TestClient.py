#######################################################################################################################
# .TestClient.py
#######################################################################################################################
# A simple CLI client to quickly test server features.
#######################################################################################################################

import socket

host, port  = '137.184.241.2', 52525
headerBytes = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])


####
# makePackage(payload)
####
# Support function to construct a byte sequence from a payload string.
####
def makePackage(payload):
    size = (4 + len(headerBytes) + len(bytes(payload, 'utf-8'))).to_bytes(4, 'big')
    return size + headerBytes + bytes(payload, 'utf-8')


####
# sendPackage(package)
####
# Support function to create a socket, send a byte sequence across it, and return the response
####
def sendPackage(package):
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
# testAddNewFile()
####
# Test the add_new_file command.
####
def testAddNewFile():
    print("TESTING [addNewFile(payload)]-----------------------------------------------------------------------------")
    print("----------------------------------------------------------------------------------------------------------")


####
# testDeleteFile()
####
# Test the delete_file command.
####
def testDeleteFile():
    print("TESTING [deleteFile(payload)]-----------------------------------------------------------------------------")
    print("----------------------------------------------------------------------------------------------------------")


####
# testLoginUser()
####
# Test the login_user command.
####
def testLoginUser():
    print("TESTING [loginUser(payload)]------------------------------------------------------------------------------")
    failures = 0
    ## Test with New User
    package = makePackage('{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"11.22.33.44:55123"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_OK')
    if response == expected:
        print("[ ] New User Test")
    else:
        print("[X] New User Test")
        failures += 1
    ## Test with Existing User, Different IP
    package = makePackage('{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"55.66.77.88:55789"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_OK')
    if response == expected:
        print("[ ] Existing User Test")
    else:
        print("[X] Existing User Test")
        failures += 1
    ## Final Report
    if failures == 0:
        print("[loginUser(payload)]: PASSES")
    else:
        print("[loginUser(payload)]: FAILED {} TESTS".format(failures))
    print("----------------------------------------------------------------------------------------------------------")
    

####
# testLogoutUser()
####
# Test the logout_user command.
####
def testLogoutUser():
    print("TESTING [logoutUser(payload)]-----------------------------------------------------------------------------")
    failures = 0
    ## Test with Existing Online User
    package = makePackage('{"Command":"logout_user", "Username":"testuser1"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_OK')
    if response == expected:
        print("[ ] Existing Online User Test")
    else:
        print("[X] Existing Online User Test")
        failures += 1
    ## Test with Existing Offline User
    package = makePackage('{"Command":"logout_user", "Username":"evan"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_OK')
    if response == expected:
        print("[ ] Existing Offline User Test")
    else:
        print("[X] Existing Offline User Test")
        failures += 1
    ## Testing with Non-Existent User
    package = makePackage('{"Command":"logout_user", "Username":"nonexistentuser1"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_IGNORE')
    if response == expected:
        print("[ ] Non-Existent User Test")
    else:
        print("[X] Non-Existent User Test")
        failures += 1
    ## Final Report
    if failures == 0:
        print("[logoutUser(payload)]: PASSES")
    else:
        print("[logoutUser(payload)]: FAILED {} TESTS".format(failures))
    print("----------------------------------------------------------------------------------------------------------")


####
# testGetUserIP()
####
# Test the get_user_ip command.
####
def testGetUserIP():
    print("TESTING [getUserIP(payload)]------------------------------------------------------------------------------")
    failures = 0
    ## Test with Existing Online User
    package = makePackage('{"Command":"get_user_ip", "TargetUser":"danielle"}')
    response = sendPackage(package)
    expected = genResponse('''('{"LatestIP":"45.45.45.45:57123"}',)''')
    if response == expected:
        print("[ ] Existing Online User Test")
    else:
        print("[X] Existing Online User Test")
        failures += 1
    ## Test with Existing Offline User
    package = makePackage('{"Command":"get_user_ip", "TargetUser":"evan"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_USER_OFFLINE')
    if response == expected:
        print("[ ] Existing Offline User Test")
    else:
        print("[X] Existing Offline User Test")
        failures += 1
    ## Test with Non-Existent User
    package = makePackage('{"Command":"get_user_ip", "TargetUser":"nonexistentuser1"}')
    response = sendPackage(package)
    expected = genResponse('STATUS_USER_OFFLINE')
    if response == expected:
        print("[ ] Non-Existent User Test")
    else:
        print("[X] Non-Existent User Test")
        failures += 1
    ## Final Report
    if failures == 0:
        print("[getUserIP(payload)]: PASSES")
    else:
        print("[getUserIP(payload)]: FAILED {} TESTS".format(failures))
    print("----------------------------------------------------------------------------------------------------------")


####
# testVerifyUserFiles()
####
# Test the verify_user_files command.
####
def testVerifyUserFiles():
    print("TESTING [verifyUserFiles(payload)]------------------------------------------------------------------------")
    failures = 0
    ## Test with One Unknown, One Missing, One OK
    package = makePackage('{"Command":"verify_user_files", "Username":"barbara", "FileList":"garbage.jnk?mona-lisa.jpg"}')
    response = sendPackage(package)
    expected = genResponse("[{'garbage.jnk': 'STATUS_UNKNOWN_FILE'}, {'mona-lisa.jpg': 'STATUS_OK'}, {'sample.db': 'STATUS_MISSING_FILE'}]")
    if response == expected:
        print("[ ] One Unknown, One Missing, One OK Test")
    else:
        print("[X] One Unknown, One Missing, One OK Test")
        failures += 1
    ## Final Report
    if failures == 0:
        print("[verifyUserFiles(payload)]: PASSES")
    else:
        print("[verifyUserFiles(payload)]: FAILED {} TESTS".format(failures))
    print("----------------------------------------------------------------------------------------------------------")



#######################################################################################################################
# Perform Tests
#######################################################################################################################

testAddNewFile()
testDeleteFile()
testLoginUser()
testLogoutUser()
testGetUserIP()
testVerifyUserFiles()
