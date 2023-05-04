#######################################################################################################################
## TCP.py
#######################################################################################################################
#### - Provides top-level server functions for sending/receiving data.
#######################################################################################################################

import json
import selectors
import socket
import struct
import types

import DB
import Log


HEADERBYTES = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])
SIZE_PREFIX_LENGTH = 4
HEADER_PREFIX_LENGTH = len(HEADERBYTES)

SELECTOR = None
CONNECTIONS = dict()


#####
# acceptConnection(sock: socket.socket)
#####
# - Accepts a remote connection on the given socket and sets it to non-blocking
# - Registers the socket with read/write events and address, request, response data
#####
def acceptConnection(sock):
    connection, address = sock.accept()
    Log.info('Client Connected: {}'.format(address))
    data = types.SimpleNamespace(address=address, response=b'')
    events = selectors.EVENT_READ | selectors.EVENT_WRITE
    SELECTOR.register(connection, events, data=data)


#####
# serviceConnection(sock: socket.socket)
#####
# - Accepts key and mask from selector events
# - Processes read events and checks if connection should be maintained
# - Processes write events, sending data over socket and resetting response field of data object
#####
def serviceConnection(key, mask):
    sock = key.fileobj
    data = key.data

    try:
        address = sock.getpeername()[0]
    except OSError:
        address = ''

    try:
        ## Handle Selector Read Event
        if mask & selectors.EVENT_READ:
            request = receive(sock)
            if request:
                data.response = process(parse(request), sock)
                Log.printRequest(request, address)
            elif DB.getUsernameFromAddress(address) not in CONNECTIONS:
                SELECTOR.unregister(sock)
                sock.close()
                return
        ## Handle Selector Write Event
        if mask & selectors.EVENT_WRITE:
            if data.response:
                send(sock, data.response)
                Log.printResponse(data.response, address)
                data.response = b''

    except ConnectionResetError:
        Log.info('ConnectionResetError Handled: {}'.format(address))
        username = DB.getUsernameFromAddress(address)
        terminateConnection(username)
        return

    except BrokenPipeError:
        Log.info('BrokenPipeError Handled: {}'.format(address))
        data.response = b''
        username = DB.getUsernameFromAddress(address)
        terminateConnection(username)
        return

    except OSError:
        Log.info('OSError Handled')
        return


#####
# maintainConnection(username: string, socket: socket.socket)
#####
# - Adds username:socket pair to connections dictionary.
#####
def maintainConnection(username, socket):
    if username not in CONNECTIONS.keys():
        CONNECTIONS[username] = socket


#####
# terminateConnection(username: string)
#####
# - Removes username:socket pair from connections dictionary.
#####
def terminateConnection(username):
    if username in CONNECTIONS.keys():
        DB.markUserOffline(username)
        del CONNECTIONS[username]


#####
# receive(socket: socket.socket)
#####
# - Receive inbound package from active network connection socket.
#####
def receive(socket):
    ## Receive Size
    messageSize = receiveAll(socket, SIZE_PREFIX_LENGTH)
    if not messageSize:
        return None
    messageSize = int.from_bytes(messageSize, 'big')

    ## Receive Header
    messageHeader = bytes(receiveAll(socket, HEADER_PREFIX_LENGTH))
    if messageHeader != HEADERBYTES:
        Log.error('Malformed Packaged Received: Header Found: {}'.format(messageHeader))
        return None

    ## Receive Payload
    payloadSize = messageSize - SIZE_PREFIX_LENGTH - HEADER_PREFIX_LENGTH
    payload = receiveAll(socket, payloadSize)

    ## If No Payload Received, Return None
    ## Else, Return Payload
    if not payload:
        return None
    else:
        payload = bytes(payload).strip()
        return bytes(payload)


#####
# receiveAll(socket: socket.socket, length: integer)
#####
# - Receives length bytes.
# - Returns None if hits EOF.
# - Returns received bytes otherwise.
#####
def receiveAll(socket, length):
    data = bytearray()
    while len(data) < length:
        try:
            packet = socket.recv(length - len(data))
            if not packet:
                return None
            data.extend(packet)
        except BlockingIOError:
            continue
    return data


#####
# send(socket: socket.socket, payload: JSON Object)
#####
# - Constructs a UTF-8 encoded bytes array from payload
# - Calculates length of final package as a 4-byte, big-endian integer
# - Sends length + payload out over active network connection socket.
#####
def send(socket, response):
    socket.sendall(response)


#####
# parse(payload: UTF-8 encoded JSON-formatted string as bytes object)
#####
# - Extracts and returns the JSON Object.
#####
def parse(payload):
    return json.loads(payload)


#####
# process(payload: JSON Object)
#####
# - Matches against command field of payload.
# - Calls into appropriate DB function to get a response.
# - Returns response if matched a valid command.
# - Returns payload otherwise.
#####
def process(payload, socket):
    if not isinstance(payload, dict):
        Log.error('Malformed Package Payload; Expected JSON Object, Received: {}'.format(payload))
        response = payload
    elif not 'Command' in payload.keys():
        Log.error('Malformed Package Payload; Missing Command Field: {}'.format(payload))
        response = payload
    else:
        command = payload['Command']
        match command:
            case 'add_new_file':
                response = DB.addNewFile(payload)
            case 'change_password':
                response = DB.changePassword(payload)
            case 'delete_file':
                response = DB.deleteFile(payload)
            case 'download_file':
                response = DB.downloadFile(payload)
            case 'login_user':
                response = DB.loginUser(payload, socket)
            case 'logout_user':
                response = DB.logoutUser(payload)
            case 'get_users_files':
                response = DB.getUsersAndFiles(payload)
            case 'get_user_ip':
                response = DB.getUserIP(payload)
            case 'send_file':
                response = DB.sendFile(payload)
            case 'verify_user_files':
                response = DB.verifyUserFiles(payload)
            case _:
                Log.error('Unsupported Command: {}'.format(command))
                response = command
    response = bytes(str(response), 'utf-8')
    length = (4 + len(response)).to_bytes(4, 'big')
    return length + response


#######################################################################################################################
# EOF
#######################################################################################################################
