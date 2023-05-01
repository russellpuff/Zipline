import base64

header = bytes([0x63, 0xc3, 0x1e, 0xe9, 0xf0, 0x58, 0x60, 0x1d])
payload = base64.b64encode('Test String'.to_bytes('big'))
length = (4 + len(header) + len(payload)).to_bytes(4, 'big')
package = length + header + payload

print('Length:  {}'.format(length))
print('Header:  {}'.format(header))
print('Payload: {}'.format(payload))
print('Package Type: {}'.format(type(package)))
print('Package: {}'.format(package))

