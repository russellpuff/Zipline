#######################################################################################################################
# ZiplineServer.py
#######################################################################################################################
#### Provides socket-based data transfer and SQLite database services to support Zipline client applications.
#######################################################################################################################

import selectors
import socket
import sys
import threading

import DB
import Log
import TCP


if __name__ == "__main__":
    ## Load Database
    DB.loadDatabase()
    if not DB.validateSchema():
        Log.error('Database has Invalid Schema: {}'.format(DB.DATABASE))
        sys.exit()
        
    
    ## Bind Socket and Listen
    host, port = '0.0.0.0', 52525
    TCP.SELECTOR = selectors.DefaultSelector()
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as lsock:
            lsock.bind((host, port))
            lsock.listen(1)
            lsock.setblocking(False)
            TCP.SELECTOR.register(lsock, selectors.EVENT_READ, data=None)
            Log.info('Listening on Address: {} and Port: {}'.format(host, port))
            while True:
                events = TCP.SELECTOR.select(timeout=None)
                for key, mask in events:
                    if key.data is None:
                        TCP.acceptConnection(key.fileobj)
                    else:
                        TCP.serviceConnection(key, mask)
    except KeyboardInterrupt:
        Log.info('Keyboard Interrupt Received; Exiting')
    finally:
        TCP.SELECTOR.close()


#######################################################################################################################
# EOF
#######################################################################################################################
