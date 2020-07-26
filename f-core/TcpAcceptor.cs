using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace f_core
{
    public interface ITcpAcceptor
    {
        string ServerName { get; }
        int Port { get; }
    }

    public class TcpAcceptor : ITcpAcceptor
    {
        private readonly IServer _server;
        private readonly TcpListener _listener;

        public static ITcpAcceptor New(IServer server)
        {
            return new TcpAcceptor(server);
        }

        private TcpAcceptor(IServer server)
        {
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();

            _server = server;
            _listener = listener;

            Task.Run(() => acceptConnections());
        }

        private async Task acceptConnections()
        {
            for (;;)
            {
                var tcp = await _listener.AcceptTcpClientAsync();
                _ = Task.Run(() => processRequest(tcp));
            }
        }

        private async Task processRequest(TcpClient tcp)
        {
            try
            {
                var op = TcpOp.New(tcp.GetStream());
                await _server.ProcessRequest(op);
            }
            catch (Exception ex)
            { }
            finally
            {
                tcp.Close();
            }
        }

        string ITcpAcceptor.ServerName 
        { 
            get { return Environment.MachineName; }
        }

        int ITcpAcceptor.Port 
        { 
            get 
            {
                var epoint = (IPEndPoint) _listener.LocalEndpoint;
                return epoint.Port;
            }
        }
    }
}
