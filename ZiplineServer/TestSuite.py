#######################################################################################################################
# TestSuite.py
#######################################################################################################################
#### Perform unit testing on the Zipline server.
#######################################################################################################################

import base64
import os
import sqlite3
import selectors
import socket
import threading

import DB
import Log
import TCP
import Test


#######################################################################################################################
# Unit Tests
#######################################################################################################################

#####
# testLoginUser()
#####
def testLoginUser():
    Test.printHeader('loginUser')
    failures = 0

    ## Test with New User
    password = str(base64.b64encode(b'123456789012345678901234567890123456'))
    response = Test.sendPackage('{"Command": "login_user","Username": "testuser1","Password":"' + password  + '"}')
    expected = Test.generateResponse('STATUS_OK')
    failures += Test.printResult('New User Test', response, expected)

    ## Test with Existing User, Different IP
    response = Test.sendPackage('{"Command": "login_user","Username": "testuser1","Password":"' + password  + '"}')
    expected = Test.generateResponse('STATUS_OK')
    failures += Test.printResult('Existing User Test', response, expected)

    Test.resetDatabase()
    Test.printFinalResults('loginUser', failures)


#####
# testLogoutUser()
#####
def testLogoutUser():
    Test.printHeader('logoutUser')
    failures = 0

    ## Test with Existing Online User
    response = Test.sendPackage('{"Command":"logout_user","Username":"albert"}')
    expected = Test.generateResponse('STATUS_OK')
    failures += Test.printResult('Existing Online User Test', response, expected)

    ## Test with Existing Offline User
    response = Test.sendPackage('{"Command":"logout_user","Username":"eduardo"}')
    expected = Test.generateResponse('STATUS_IGNORE')
    failures += Test.printResult('Existing Offline User Test', response, expected)

    ## Test with Non-Existent User
    response = Test.sendPackage('{"Command":"logout_user","Username":"nonexistentuser"}')
    expected = Test.generateResponse('STATUS_IGNORE')
    failures += Test.printResult('Non-Existent User Test', response, expected)

    Test.resetDatabase()
    Test.printFinalResults('logoutUser', failures)


#####
# testGetUserIP()
#####
def testGetUserIP():
    Test.printHeader('getUserIP')
    failures = 0

    ## Test with Existing Online User
    response = Test.sendPackage('{"Command":"get_user_ip","TargetUser":"albert"}')
    expected = Test.generateResponse('''('{"LatestIP":"1.2.3.4:55555"}',)''')
    failures += Test.printResult('Existing Online User Test', response, expected)

    ## Test with Existing Offline User
    response = Test.sendPackage('{"Command":"get_user_ip","TargetUser":"eduardo"}')
    expected = Test.generateResponse('STATUS_USER_OFFLINE')
    failures += Test.printResult('Existing Offline User Test', response, expected)

    ## Test with Non-Existent User
    response = Test.sendPackage('{"Command":"get_user_ip", "TargetUser":"nonexistentuser"}')
    expected = Test.generateResponse('STATUS_USER_OFFLINE')
    failures += Test.printResult('Non-Existent User Test', response, expected)

    Test.resetDatabase()
    Test.printFinalResults('getUserIP', failures)


#######################################################################################################################
# Testing
#######################################################################################################################
#### - Create a new database file according to schema defined in SQL.py.
#### - Starts a server listening on 127.0.0.1:52525 for testing.
#### - Perform unit tests of server features, initializing database with test data as needed.
#### - Reset database after every unit test to prevent unanticipated test interactions.
#### - Delete database file after testing.
#######################################################################################################################

def serverThread():
    host, port = '0.0.0.0', 52525
    TCP.SELECTOR = selectors.DefaultSelector()

    ## Bind Socket and Listen
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as lsock:
            lsock.bind((host, port))
            lsock.listen(1)
            lsock.setblocking(False)
            TCP.SELECTOR.register(lsock, selectors.EVENT_READ, data=None)
            Log.info('Listening on Address: {} and Port: {}'.format(host, port))
            while TESTING:
                events = TCP.SELECTOR.select(timeout=None)
                for key, mask in events:
                    if key.data is None:
                        TCP.acceptConnection(key.fileobj)
                    else:
                        TCP.serviceConnection(key, mask)
    except KeyboardInterrupt:
        Log.info('Keyboard Interrupt Received; Exiting')
    except ConnectionRefusedError:
        Log.error('ConnectionRefusedError Handled')
        TESTING = False
    finally:
        TCP.SELECTOR.close()


if __name__ == '__main__':
    ## Set Database Module to Load a Separate Test Database File
    DB.DATABASE = '.TestDatabase.db'
    DB.loadDatabase()
    Test.resetDatabase()
    Log.info('Using {} for Testing'.format(DB.DATABASE))

    ## Start Test Server in Separate Thread
    TESTING = True
    thread = threading.Thread(target=serverThread)
    thread.start()

    ## Run Unit Tests
    testLoginUser()
    testLogoutUser()
    testGetUserIP()

    ## Shutdown the Server
    Log.info('Shutting Down the Test Server')
    TESTING = False
    thread.join()

    ## Delete Test Database
    if os.path.isfile(DB.DATABASE):
        os.remove(DB.DATABASE)
        Log.info('Removing {} Test Database'.format(DB.DATABASE))
    else:
        Log.error('Could Not Delete {}; File Not Found'.format(DB.DATABASE))


#######################################################################################################################
# EOF
#######################################################################################################################
