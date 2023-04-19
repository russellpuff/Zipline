import socket
import sys

HOST, PORT = "137.184.241.2", 52525

header = bytes([0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D])
payload = b'{"Command":"logout_user","Username":"testuser"}'
size = (4 + len(header + payload)).to_bytes(4, 'big')
package = size + header + payload

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as sock:
    # Connect to Server and Send Data
    sock.connect((HOST, PORT))
    sock.sendall(package)

    # Receive Data from the Server and Shut Down
    received = str(sock.recv(1024))

print("OUTBOUND--------------------------------------------")
print("Sent:      {}".format(package))
print("Size:      {}".format(int.from_bytes(size, 'big')))
print()

print("INBOUND---------------------------------------------")
print(received)
