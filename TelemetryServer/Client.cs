using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using GSLib.Collections;

namespace TelemetryServer
{
    public class Client
    {
        TcpClient client;
        Thread clientThread;

        public IPEndPoint ServerEndpoint { get; private set; }
        public bool AwaitingConnection { get; private set; }
        public bool Listening { get; private set; }
        public bool HasConnection
        {
            get
            {
                return client != null ? client.Connected : false;
            }
        }

        public EventHandler<DataAvailableEventArgs> DataAvailable;
        public EventHandler Connected;
        public EventHandler Disconnected;

        public Client(IPEndPoint serverEndpoint)
        {
            ServerEndpoint = serverEndpoint;
        }

        public void StopConnecting()
        {
            AwaitingConnection = false;
        }

        public void StartConnecting()
        {
            client = new TcpClient();
            clientThread = new Thread(new ThreadStart(CollectData));
            clientThread.Start();
        }

        public void Disconnect()
        {
            Listening = false;
            AwaitingConnection = false;
        }

        void CollectData()
        {
            AwaitingConnection = true;
            while (AwaitingConnection)
            {
                while (!Connect() & AwaitingConnection) { }
                AwaitingConnection = false;
                if (client.Connected)
                {
                    if (Connected != null) Connected(this, new EventArgs());
                    ListenForData();
                }
            }
        }

        bool Connect()
        {
            client = new TcpClient();

            try
            {
                client.Connect(ServerEndpoint);
            }
            catch
            {
                return false;
            }


            while (!client.Connected & AwaitingConnection) { }
            //Trace.WriteLine("Connected");

            //Trace.WriteLine("Client listening on: " + client.Client.LocalEndPoint.ToString());
            return true;
        }

        void ListenForData()
        {
            //Trace.WriteLine("Reading data.");
            Stream clientStream = client.GetStream();
            BinaryReader reader = new BinaryReader(clientStream);

            DataBlock data;
            Protocols protocol;

            Queue<byte> rawData = new Queue<byte>();
            int length = 0;
            byte[] magicNumber = new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E };

            reader = new BinaryReader(clientStream);

            Listening = true;

            Action<int> enqueueByte = new Action<int>(x => { while (rawData.Count < x) rawData.Enqueue(reader.ReadByte()); });

            while (client.Connected & Listening)
            {
                try
                {
                    enqueueByte(magicNumber.Length);
                    while (Helpers.CompareArrays<byte>(rawData.ToArray(), magicNumber) != 0)
                    {
                        rawData.Dequeue();
                        enqueueByte(magicNumber.Length);
                    }
                    rawData.Clear();
                    enqueueByte(1);
                    protocol = (Protocols)rawData.Dequeue();
                    enqueueByte(4);
                    length = BitConverter.ToInt32(rawData.ToArray(), 0);
                    rawData.Clear();
                    enqueueByte(length);
                    //Trace.WriteLine("Reading " + length.ToString() + " bytes.");
                    //data = TelemetryData.Deserialize(rawData.ToArray());
                    data = new DataBlock(protocol, rawData.ToArray());
                    rawData.Clear();
                    if (DataAvailable != null) DataAvailable(this, new DataAvailableEventArgs(data));
                }
                catch
                {
                    break;
                }
            }

            Listening = false;
            client.GetStream().Close();
            client.Close();
            if (Disconnected != null) Disconnected(this, new EventArgs());
        }
    }
}