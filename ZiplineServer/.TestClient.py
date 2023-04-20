#######################################################################################################################
# .TestClient.py
#######################################################################################################################
# A simple CLI client to quickly test server features.
#######################################################################################################################

import socket

host, port  = '137.184.241.2', 52525
headerBytes = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])

def testLoginUser(socket):
    print("TESTING [loginUser(payload)]------------------------------------------------------------------------------")
    failures = 0
    ## Test with New User
    payload = b'{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"11.22.33.44:55123"}'
    size = (4 + len(headerBytes) + len(payload)).to_bytes(4, 'big')
    package = size + headerBytes + payload
    socket.connect((host, port))
    socket.sendall(package)
    response = str(sock.recv(1024))
    expected = b'STATUS_OK'
    if response == expected:
        print("[ ] New User Test")
    else:
        print("[X] New User Test")
        failures += 1
    ## Test with Existing User, Different IP
    payload = b'{"Command":"login_user", "Username":"testuser1", "Password":"null", "LatestIP":"55.66.77.88:55789"}'
    size = (4 + len(headerBytes) + len(payload)).to_bytes(4, 'big')
    package = size + headerBytes + payload
    socket.connect((host, port))
    socket.sendall(package)
    response = str(sock.recv(1024))
    expected = b'STATUS_OK'
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

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as sock:
    testLoginUser(sock)
