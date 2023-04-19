###################################################################################################
# ZiplineServer.py
###################################################################################################
# Provides socket-based data transfer and SQLite database services to support
# Zipline client applications.
###################################################################################################

import json
import socketserver
import sqlite3



###################################################################################################
# SQL Statements
###################################################################################################

sqlGetUsersAndFiles = '''
    SELECT      json_group_array(
                    json_object(
                    'FileGUID', FILES.FILE_GUID,
                    'Username', USERS.USERNAME,
                    'Filename', FILES.FILENAME,
                    'FileSize', FILES.FILESIZE
                    )
                )
    FROM        FILES
    INNER JOIN  USERS ON FILES.USER_ID = USERS.USER_ID
    WHERE       USERS.ONLINESTATUS = 1;
'''

sqlUserExists = '''
    SELECT  *
    FROM    USERS
    WHERE   USERNAME = ?;
'''

sqlInsertOnlineUser = '''
    INSERT  INTO USERS (USERNAME, PW_HASH, ONLINESTATUS, LATEST_IP)
    VALUES            (?, ?, 1, ?);'''

sqlUpdateUserOnline = '''
    UPDATE  USERS
    SET     LATEST_IP = ?, ONLINESTATUS = 1
    WHERE   USERNAME = ?;
'''

sqlUpdateUserOffline = '''
    UPDATE  USERS
    SET     ONLINESTATUS = 0
    WHERE   USERNAME = ?;
'''

sqlGetOnlineUsers = '''
    SELECT  LATEST_IP
    FROM    USERS
    WHERE   ONLINESTATUS = 1;
'''

###################################################################################################
# Database Functions
###################################################################################################

database = 'Zipline.db'

def loginUser(payload):
    username = payload['Username']
    password = payload['Password']
    latestip = payload['LatestIP']

    dbConnection = sqlite3.connect(database)
    dbCursor = dbConnection.cursor()

    # Check if User in Table
    result = dbCursor.execute(sqlUserExists, [username])

    # If User Not in Table, Add Entry
    # Otherwise, Update Existing Entry
    if (result.fetchone() is None):
        dbCursor.execute(sqlInsertOnlineUser, [username, password, latestip])
    else:
        dbCursor.execute(sqlUpdateUserOnline, [latestip, username])
    
    dbConnection.commit()
    dbConnection.close()
    return 'STATUS_OK'

def logoutUser(payload):
    username = payload['Username']
    
    dbConnection = sqlite3.connect(database)
    dbCursor = dbConnection.cursor()

    result = dbCursor.execute(sqlUserExists, [username])
    if (result.fetchone() is None):
        return 'STATUS_IGNORE'
    else:
        dbCursor.execute(sqlUpdateUserOffline, [username])

    dbConnection.commit()
    dbConnection.close()
    return 'STATUS_OK'


def getUsersAndFiles():
    dbConnection = sqlite3.connect(database)
    dbCursor = dbConnection.cursor()
    result = dbCursor.execute(sqlGetUsersAndFiles).fetchall()
    dbConnection.commit()
    dbConnection.close()
    return result



###################################################################################################
# Network Protocols and Packet Handler
###################################################################################################

HEADER_BYTES = [0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D]
SERVER_HOST = '0.0.0.0'
SERVER_PORT = 52525

class TCPHandler(socketserver.BaseRequestHandler):
    """
    Instantiated once per connection to the server; must override the handle() method to
    implement communication to the client.
    """

    ## Print the Inbound Package Data to Standard Out
    def printInbound(self, data):
        print("INBOUND---------------------------------------")
        print("Size:     {}".format(int.from_bytes(data[0:4], 'big')))
        print("Header:   {}".format(data[4:12]))
        print("Received: {}".format(data))
        print()


    ## Print the Outbound Package Data to Standard Out
    def printOutbound(self, data):
        print("OUTBOUND--------------------------------------")
        print("Size:     {}".format(int.from_bytes(data[0:4], 'big')))
        print("Header:   {}".format(data[4:12]))
        print("Sent:     {}".format(data))
        print()


    ## Receive Inbound Package from Network Socket
    def receive(self):
        inbound = self.request.recv(1024).strip()
        self.printInbound(inbound)
        return inbound    


    ## Parse Command from Inbound Package
    def parse(self, inbound):
        # Validate Package Structure; Return None if Malformed
        size = int.from_bytes(inbound[0:4], 'big')
        header = inbound[4:12]
        if size < len(HEADER_BYTES) or header != bytes(HEADER_BYTES):
            print("[ERROR] Malformed Inbound Data")
            return None

        # Return Command String if Package Structure is Valid
        payload = json.loads(inbound[12:])
        return payload


    ## Process Command and Return Outbound Package
    def process(self, payload):
        match payload['Command']:
            case 'login_user':
                return bytes(str(loginUser(payload)), 'utf-8')
            case 'logout_user':
                return bytes(str(logoutUser(payload)), 'utf-8')
            case 'get_users_files':
                return bytes(str(getUsersAndFiles()), 'utf-8')
            case _:
                return command


    ## Send Outbound Package via Network Socket
    def send(self, outbound):
        self.printOutbound(outbound)
        self.request.sendall(outbound)


    ## Top-Level Logic for Handling Client Requests
    def handle(self):
        # Receive Inbound Package and Parse Command
        inbound = self.receive()
        indata = self.parse(inbound)

        # Construct and Send Outbound Package
        payload = self.process(indata)
        length = (4 + len(payload)).to_bytes(4, 'big')
        self.send(length + payload)

        dbConnection = sqlite3.connect(database)
        dbCursor = dbConnection.cursor()
        result = dbCursor.execute('SELECT * FROM USERS;').fetchall()
        print(result)


###################################################################################################
# Open TCP/IP Socket; Listen and Respond to Requests
###################################################################################################

if __name__ == "__main__":
    HOST, PORT = SERVER_HOST, SERVER_PORT
    with socketserver.TCPServer((HOST, PORT), TCPHandler) as server:
        server.serve_forever()
