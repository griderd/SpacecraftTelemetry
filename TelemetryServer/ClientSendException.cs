using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TelemetryServer
{
    public class ClientSendException : Exception
    {
        public Socket ClientSocket { get; private set; }

        public ClientSendException(string message, Socket clientSocket, Exception innerException)
            : base(message, innerException)
        {
            ClientSocket = clientSocket;
        }
    }
}
