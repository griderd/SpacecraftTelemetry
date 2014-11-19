using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RawClient
{
    class Program
    {
        static StreamReader reader;
        static TcpClient client;
        static StreamWriter log;
        List<string> headers;

        static bool listening = false;

        static void Main(string[] args)
        {
            List<string> headers = new List<string>();
            Console.Title = "Raw Telemetry";
            log = new StreamWriter(new FileStream("log.csv", FileMode.Create, FileAccess.Write));

            Console.WriteLine("Please exit the program by pressing CTRL+C");

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
            Console.WriteLine("Program ending.");
            listening = false;
            log.Close();
            log.Dispose();
            client.Close();
        }

        static bool Connect(string ipAddress, int port, bool writeAttempt)
        {
            client = new TcpClient();
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            if (writeAttempt) Console.Write("Trying to connect to " + serverAddress.ToString() + "... ");
            try
            {
                client.Connect(serverAddress);
            }
            catch
            {
                return false;
            }

            while (!client.Connected) { }
            Console.WriteLine("Connected");

            Console.WriteLine("Client listening on: " + client.Client.LocalEndPoint.ToString());
            return true;
        }

        static void ListenForData()
        {
            StringBuilder header = new StringBuilder();
            StringBuilder line = new StringBuilder();

            bool headerWritten = false;
            bool blockStarted = false;

            // char[] c = new char[1];
            //int charsRead = 0;

            reader = new StreamReader(client.GetStream());

            listening = true;

            while (client.Connected & listening)
            {
                try
                {
                    string l = reader.ReadLine();
                    //if (l != null) lines.Add(l);
                    //Console.WriteLine(l);

                    if (l == null) return;

                    if ((l == "end") & (headerWritten) & (blockStarted))
                    {
                        PrintLine(line.ToString());
                        line.Clear();
                        blockStarted = false;
                    }
                    else if ((l == "end") & (!headerWritten) & (blockStarted))
                    {
                        PrintLine(header.ToString());
                        PrintLine(line.ToString());
                        line.Clear();
                        header.Clear();
                        headerWritten = true;
                        blockStarted = false;
                    }
                    else if ((l == "begin") & (!blockStarted))
                    {
                        blockStarted = true;
                    }
                    else if (blockStarted)
                    {
                        string[] parts = l.Split(':');
                        
                        if (line.Length > 0) line.Append(',');
                        line.Append(parts[1]);

                        if (!headerWritten)
                        {
                            if (header.Length > 0) header.Append(',');
                            header.Append(parts[0]);
                        }
                    }
                }
                catch (IOException)
                {
                    break;
                }

                //if (charsRead > 0)
                //{
                    
                //}
                //else
                //{
                //    // Console.WriteLine("End of stream!");
                //}
            }
            Console.WriteLine("Connection lost.");
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
