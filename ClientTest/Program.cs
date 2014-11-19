using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using TelemetryServer;
using GSLib.Collections;

namespace ClientTest
{
    class Program
    {
        static BinaryReader reader;
        static TcpClient client;
        static StreamWriter log;

        static bool listening = false;

        static void Main(string[] args)
        {
            Console.Title = "Client Test";
            log = new StreamWriter(new FileStream("log.txt", FileMode.Create, FileAccess.Write));

            PrintLine("Please exit the program by pressing CTRL+C");

            bool printConnectToLog = true;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            for (; ; )
            {
                while (!Connect("127.0.0.1", 8080, printConnectToLog)) { printConnectToLog = false; }
                ListenForData();
                printConnectToLog = true;
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            PrintLine();
            PrintLine("Program ending.");
            listening = false;
            log.Close();
            log.Dispose();
            client.Close();
        }

        static bool Connect(string ipAddress, int port, bool writeAttempt)
        {
            client = new TcpClient();
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            if (writeAttempt) Print("Trying to connect to " + serverAddress.ToString() + "... ");
            try
            {
                client.Connect(serverAddress);
            }
            catch
            {
                return false;
            }
            
            while (!client.Connected) { }
            PrintLine("Connected");

            PrintLine("Client listening on: " + client.Client.LocalEndPoint.ToString());
            return true;
        }

        static void ListenForData()
        {
            Queue<byte> rawData = new Queue<byte>();
            TelemetryData data;
            int length = 0;
            byte[] magicNumber = new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E };

            reader = new BinaryReader(client.GetStream());

            listening = true;

            Action<int> enqueueByte = new Action<int>(x => { while (rawData.Count < x) rawData.Enqueue(reader.ReadByte()); });

            while (client.Connected & listening)
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
                    enqueueByte(4);
                    length = BitConverter.ToInt32(rawData.ToArray(), 0);
                    rawData.Clear();
                    enqueueByte(length);
                    data = TelemetryData.Deserialize(rawData.ToArray());
                    rawData.Clear();

                    PrintLine(data.vesselName);
                    PrintLine(data.altitude.ToString("F2"));
                    PrintLine(data.totalCurrentThrust.ToString("F2"));
                    PrintLine(data.geeForce.ToString());
                    PrintLine(data.clientEndpoints.ToString());
                }
                catch (IOException)
                {
                    break;
                }   
            }
            PrintLine("Connection lost.");
            listening = false;
            client.Close();
        }

        static void Print(char c)
        {
            Console.Write(c);
            log.Write(c);
        }

        static void Print(string message)
        {
            Console.Write(message);
            log.Write(message);
        }

        static void PrintLine()
        {
            Console.WriteLine();
            log.WriteLine();
        }

        static void PrintLine(string message)
        {
            Console.WriteLine(message);
            log.WriteLine(message);
        }
    }
}
