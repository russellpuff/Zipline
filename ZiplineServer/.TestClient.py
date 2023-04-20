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
    


#######################################################################################################################
# Perform Tests
#######################################################################################################################

testLoginUser()
