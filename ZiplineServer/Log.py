#######################################################################################################################
# Log.py
#######################################################################################################################
#### -- Provides basic server logging functions.
#######################################################################################################################

LARGE_PACKAGE_MINIMUM_SIZE = 1000

#####
# error(message: string)
#####
# - Prints server error messages in a standard format.
#####
def error(message):
    print('[ERROR]')
    print('|--Message:      {}'.format(message))
    print()
    print()


#####
# info(message: string)
#####
# - Prints server info messages in a standard format.
#####
def info(message):
    print('[INFO]')
    print('|--Message:      {}'.format(message))
    print()
    print()


#####
# printRequest(package: bytes)
#####
# - Prints a request header and some of the request data.
# - Prints the full request bytes array as a UTF-8 string.
#####
def printRequest(package):
    if package:
        size = int.from_bytes(package[0:4], 'big')
        print('[INBOUND REQUEST]')
        print('|--Size:         {}'.format(size))
        print('|--Header:       {}'.format(package[4:12]))
        if (size > LARGE_PACKAGE_MINIMUM_SIZE):
            print('|--Received:  [Large Package]')
        else:
            print('|--Received:     {}'.format(package))
        print()
        print()


#####
# printResponse(package: bytes)
#####
# - Prints a response header and some of the response data.
# - Prints the full response bytes array as a UTF-8 string.
#####
def printResponse(package):
    if package:
        print('[OUTBOUND RESPONSE]')
        print('|--Size:         {}'.format(len(package)))
        if (len(package) > LARGE_PACKAGE_MINIMUM_SIZE):
            print('|--Transmitted:  [Large Package]')
        else:
            print('|--Transmitted:  {}'.format(package))
        print()
        print()


#######################################################################################################################
# EOF
#######################################################################################################################
