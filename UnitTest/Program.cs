using System;
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using TelemetryRadio;
using TelemetryServer;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 8080);
            server.Start();

            TelemetryData data = new TelemetryData();
            data.vesselName = "DummyVessel";

            while (server.Clients.Length == 0) { }
            server.Send(data);

        }
    }
}
