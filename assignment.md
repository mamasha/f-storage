
Hello and welcome :)

You will need to create a small file storage server and a client for it using the .net framework.
The system must use tcp sockets, any 3rd party file transfer protocols or their implementations are not allowed.

A client app shall connect to a server app, and identify itself using user/pass of the user.

Once connected, a server provides a dedicated folder to the user where the user can store personal files. (No need to create subfolders, just plain list of files is enough)

Through the client app graphic interface the user should be able to perform following operations in the provided server storage.

- See list of stored files
- Upload files to the server.
- Download files from the server.
- Delete files from the server.

All of the aforementioned operations are locked into the user's folder on the server, this means he must not access files of other users or even know who the other users of the system are

Only authorized users can access the server. The authorization and permissions shall be managed via the server application gui itself.

For each user an admin will define user/pass and a folder where the user’s data will be stored.

The admin can change a password for a user or even delete a user from the system.

The user information (name/pass/path) shall be stored in sqlite database.

The server will record every operation that any user or admin does on the server to a file named audit.log.

The log should be written to the filesystem in a separate thread in order not to stall any operations.

Good luck!
