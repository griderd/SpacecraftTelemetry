using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using TelemetryServer;
using System.IO;

namespace TelemetryServer
{
    class Program
    {
        static Server server;

        static void Main(string[] args)
        {
            server = new Server(IPAddress.Parse("127.0.0.1"), 8080);
            server.ClientConnected += new EventHandler<ClientConnectEventArgs>(server_ClientConnected);
            Console.WriteLine("Server listening on " + server.ServerEndpoint.ToString());
            
            //server.EndListening();

            Console.WriteLine("Press Enter to transmit.");
            Console.ReadLine();
            Console.WriteLine("Server sending \"Hello World!\\n\"");
            try
            {
                server.Send("Hello world!\n");
            }
            catch (ClientSendException ex)
            {
                Console.WriteLine("Could not send data to " + ex.ClientSocket.RemoteEndPoint.ToString());
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            server.StopServer();
        }

        static void server_ClientConnected(object sender, ClientConnectEventArgs e)
        {
            Console.WriteLine("Client connected: " + e.ClientSocket.RemoteEndPoint.ToString());
        }
    }
}
