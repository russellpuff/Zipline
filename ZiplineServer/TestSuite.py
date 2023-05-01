#######################################################################################################################
# TestSuite.py
#######################################################################################################################
#### Perform unit testing on the Zipline server.
#######################################################################################################################

import os
import sqlite3
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
    response = Test.sendPackage('''{"Command": "login_user","Username": "testuser1","Password": "null","LatestIP": "1.2.3.4:55555"}''')
    expected = Test.generateResponse('STATUS_OK')
    failures += Test.printResult('New User Test', response, expected)

    ## Test with Existing User, Different IP
    response = Test.sendPackage('''{"Command": "login_user","Username": "testuser1","Password": "null","LatestIP": "5.6.7.8:22222"}''')
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
    expected = Test.generateResponse('STATUS_OK')
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

## Set Database to Load a Separate Testing File
DB.DATABASE = '.TestDatabase.db'
DB.loadDatabase()
Test.resetDatabase()
Log.info('Using {} for Testing'.format(DB.DATABASE))


## Start Threaded Testing Server
with TCP.ThreadedTCPServer((Test.HOST, Test.PORT), TCP.RequestHandler) as server:
    server_thread = threading.Thread(target=server.serve_forever)
    server_thread.daemon = True
    server_thread.start()
    Log.info("Threaded Server Loop Running in Thread: {}".format(server_thread.name))

    ## Run Unit Tests
    testLoginUser()
    testLogoutUser()
    testGetUserIP()

    ## Shutdown Server
    Log.info('Shutting Down Test Server')
    server.shutdown()


## Delete Test Database
if os.path.isfile(DB.DATABASE):
    os.remove(DB.DATABASE)
    Log.info('Removing {} Test Database'.format(DB.DATABASE))
else:
    Log.error('Could Not Delete {}; File Not Found'.format(DB.DATABASE))


#######################################################################################################################
# EOF
#######################################################################################################################
