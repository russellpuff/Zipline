###################################################################################################
# ZiplineServer.py
###################################################################################################
# Provides socket-based data transfer and SQLite database services to support
# Zipline client applications.
###################################################################################################

import DB
import json
import socketserver


###################################################################################################
# Network Protocols and Packet Handler
###################################################################################################

headerBytes = [0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D]
host, port = '0.0.0.0', 52525

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
        if size < len(headerBytes) or header != bytes(headerBytes):
            print("[ERROR] Malformed Inbound Data")
            return None

        # Return Command String if Package Structure is Valid
        payload = json.loads(inbound[12:])
        return payload


    ## Process Command and Return Outbound Package
    def process(self, payload):
        match payload['Command']:
            case 'add_new_file':
                return bytes(str(DB.addNewFile(payload)), 'utf-8')
            case 'delete_file':
                return bytes(str(DB.deleteFile(payload)), 'utf-8')
            case 'login_user':
                return bytes(str(DB.loginUser(payload)), 'utf-8')
            case 'logout_user':
                return bytes(str(DB.logoutUser(payload)), 'utf-8')
            case 'get_users_files':
                return bytes(str(DB.getUsersAndFiles()), 'utf-8')
            case 'get_user_ip':
                return bytes(str(DB.getUserIP(payload)), 'utf-8')
            case 'verify_user_files':
                return bytes(str(DB.verifyUserFiles(payload)), 'utf-8')
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


###################################################################################################
# Open TCP/IP Socket; Listen and Respond to Requests
###################################################################################################

if __name__ == "__main__":
    with socketserver.TCPServer((host, port), TCPHandler) as server:
        server.serve_forever()
