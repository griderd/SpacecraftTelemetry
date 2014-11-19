﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TelemetryServer
{
    public class ClientConnectEventArgs : EventArgs
    {
        public Socket ClientSocket { get; private set; }

        public ClientConnectEventArgs(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }
    }

    public class Server
    {
        TcpListener listener;
        IPEndPoint serverEndpoint;
        bool listening;
        List<Socket> clients;
        Thread listenerThread;

        public event EventHandler<ClientConnectEventArgs> ClientConnected;

        public IPEndPoint ServerEndpoint
        {
            get
            {
                return serverEndpoint;
            }
        }

        public bool IsListening
        {
            get
            {
                return listening;
            }
        }

        public IPEndPoint[] Clients
        {
            get
            {
                List<IPEndPoint> clientEndPoints = new List<IPEndPoint>();
                Socket[] clientList = clients.ToArray();
                foreach (Socket client in clientList)
                {
                    clientEndPoints.Add((IPEndPoint)client.RemoteEndPoint);
                }
                return clientEndPoints.ToArray();
            }
        }



        public Server(string address, int port)
            : this(IPAddress.Parse(address), port)
        {

        }

        public Server(IPAddress address, int port)
        {
            clients = new List<Socket>();

            serverEndpoint = new IPEndPoint(address, port);

            listener = new TcpListener(serverEndpoint);
            listening = false;
            Start();
        }

        public void BeginListening()
        {
            if (!listening)
            {
                listening = true;
                listenerThread = new Thread(new ThreadStart(ListenForConnection));
                listenerThread.Start();
            }
        }

        private void ListenForConnection()
        {
            while (listening)
            {
                if (listener.Pending())
                {
                    Socket client = listener.AcceptSocket();
                    clients.Add(client);
                    if (ClientConnected != null) 
                        ClientConnected(this, new ClientConnectEventArgs(client));
                }
                Thread.Sleep(500);
            }
        }

        public void EndListening()
        {
            listening = false;
        }

        public void Start()
        {
            clients.Clear();
            listener.Start();
            BeginListening();
        }

        public void StopServer()
        {
            EndListening();
            listener.Stop();
            clients.Clear();
        }

        public int Send(TelemetryData data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] rawData;
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                rawData = stream.ToArray();

                int sentData = 0;

                try
                {
                    sentData = Send(new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E }); // Send magic number "begin"
                    sentData += Send(BitConverter.GetBytes(rawData.Length));
                    sentData += Send(rawData);
                }
                catch
                {
                    throw;
                }

                return sentData;
            }
        }

        public int Send(string data)
        {
            byte[] rawData = Encoding.UTF8.GetBytes(data);

            try
            {
                return Send(rawData);
            }
            catch
            {
                throw;
            }
        }

        public int Send(byte[] data)
        {
            bool sent = false;

            foreach (Socket client in clients)
            {
                try
                {
                    if (client.Connected)
                    {
                        client.Send(data);
                        sent = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new ClientSendException("Could not send message to client socket.", client, ex);
                }
            }

            if (sent) return data.Length;
            else return 0;
        }
    }
}
