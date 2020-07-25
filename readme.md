## f-storage test project

- [Client side Gui](#client-side-gui)
- [Server side Gui](#server-side-gui)
- [Threading model](#threading-model)
- [Software layers](#software-layers)
- [Error handling](#error-handling)
- [Protocol](#protocol)
- [Security concerns](#security concerns)
- [Other concerns](#other-concerns)
- [Coding convention](#coding-convention)
- [Deployment](#deployment)
- [Project structure](#project-structure)
- [Programming to interfaces](#programming-to-interfaces)
- [Third parties](#third-parties)
- [Unit tests](#unit-tests)
- [High level design](#high-level-design)
- [Class level design](#class-level-design)


### Client side GUI
![Client](./client-gui.png)

### Server side GUI
![Server](./server-gui.png)

### Threading model
- Async/Await

### Software layers

### Other concerns 
- 

### Error handling

### Protocol
- Transaction per connection

### Coding convention

### Deployment

### Project structure

### Programming to interfaces

### Security concerns

### Third parties

### Unit tests

### High level design

### Class level design

interface IAuthenticator {
    void Authenticate(Request request);
}

interface ILogger {
    void Log(object msg);
}


interface IServer {
    void AddUser(string userName, string password, string folder);
    void RemoveUser(string userName);
    void UpdatePassword(string userName, string password);
}

interface IClient {
    string[] ListFiles(SrvRequest request);
    void Upload(SrvRequest request, string srcPath);
    void Download(SrvRequest request, string fileName, string dstFolder);
    void Delete(SrvRequest request, string fileName);
}

class TcpProxy : IClient
{
    private readonly IClient _server;
    
    public static IClient MakeClientSide(string address);
    
    public static void StartServerSide(IClient client)
    {
        
    }
    
    private void ProcessEvent()
    {
        var tcp = ListenForConnections();
        var request = tcp.ReadRequest();
        switch (request.OpCode)
        {
            case "ListFiles": 
                SrvListFiles(); 
                return;
        }
    }
    
    private void SrvListFiles(Tcp tcp)
    {
        var list = _server.ListFiles(request);
        tcp.WriteStringArray(list);
    }
    
    public ListFiles()
    {
        var tcp = makeTcpConnection();
        tcp.WriteRequest(request);
        var list = tcp.ReadStringArray();
        return list;
    }
}

class Server : IClient, IServer
{
    
}