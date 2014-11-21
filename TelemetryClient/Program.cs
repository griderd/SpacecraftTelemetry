using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TelemetryServer;
using System.IO;
using GSLib.Collections;
using System.Diagnostics;

namespace TelemetryClient
{
    enum CheckTristate
    {
        Undetermined,
        NoGo,
        Go
    }

    static class Program
    {
        public static CraftConfig cfg = null;
        
        public static double countdownTime;
        public static bool runCountdown;

        public static TelemetryData data;
        public static Dictionary<frmControlTerminal, CheckTristate> stationsGo;
        static TcpClient client;

        public static bool Connected { get { return client.Connected; } }
        public static bool listening;
        public static bool tryToConnect;

        public static IPEndPoint ServerAddress { get; set; }

        static Thread t;

        public static frmBooster booster;
        public static frmFlightDirector flight;
        public static frmFlightDirector aflight;
        public static frmRetro retro;
        public static frmFDO fdo;
        public static frmGuido guido;
        public static frmSurgeon surgeon;
        public static frmCapcom capcom;
        public static frmEECOM eecom;
        public static frmGNC gnc;
        public static frmTelmu telmu;
        public static frmControl control;
        public static frmInco inco;
        public static frmProcedures procedures;
        public static frmFAO fao;
        public static frmNetwork network;

        public static ResourceInfo GetResourceInfo(string resourceName)
        {
            if (data.totalResources != null)
            {
                foreach (ResourceInfo resource in data.totalResources)
                {
                    if (resource.resourceName == resourceName) return resource;
                }
            }
            return new ResourceInfo();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServerAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            stationsGo = new Dictionary<frmControlTerminal, CheckTristate>();
            listening = false;
            tryToConnect = false;

            t = new Thread(new ThreadStart(CollectData));
            t.Start();

            countdownTime = 0;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeForms();
            Application.Run(new frmMain());

            t.Abort();
            booster.Close();
            retro.Close();
            fdo.Close();
            guido.Close();
            surgeon.Close();
            capcom.Close();
            eecom.Close();
            gnc.Close();
            telmu.Close();
            control.Close();
            inco.Close();
            procedures.Close();
            flight.Close();
            aflight.Close();
            fao.Close();
            network.Close();
        }

        static void InitializeForms()
        {
            InitializeForm<frmBooster>(ref booster);
            InitializeForm<frmRetro>(ref retro);
            InitializeForm<frmFDO>(ref fdo);
            InitializeForm<frmGuido>(ref guido);
            InitializeForm<frmSurgeon>(ref surgeon);
            InitializeForm<frmCapcom>(ref capcom);
            InitializeForm<frmEECOM>(ref eecom);
            InitializeForm<frmGNC>(ref gnc);
            InitializeForm<frmTelmu>(ref telmu);
            InitializeForm<frmControl>(ref control);
            InitializeForm<frmInco>(ref inco);
            InitializeForm<frmProcedures>(ref procedures);
            InitializeForm<frmFlightDirector>(ref aflight);
            InitializeForm<frmFlightDirector>(ref flight);
            InitializeForm<frmFAO>(ref fao);
            InitializeForm<frmNetwork>(ref network);
        }

        static void InitializeForm<T>(ref T form) 
            where T : frmControlTerminal, new()
        {
            form = System.Activator.CreateInstance<T>();
            stationsGo.Add(form, CheckTristate.Undetermined);
        }

        public static void ResetGoNoGo()
        {
            frmControlTerminal[] stations = stationsGo.Keys.ToArray();
            for (int i = 0; i < stations.Length; i++)
            {
                frmControlTerminal station = stations[i];
                station.ResetGoNoGo();
                stationsGo[station] = CheckTristate.Undetermined;
            }
        }

        public static void SetGo(frmControlTerminal source, CheckTristate value)
        {
            stationsGo[source] = value;
        }

        public static void BeginCollectData()
        {
            t = new Thread(new ThreadStart(CollectData));
            t.Start();
        }

        public static void EndCollectData()
        {
            listening = false;
            tryToConnect = false;
        }

        static void CollectData()
        {
            Trace.WriteLine("Beginning data collection.");
            tryToConnect = true;
            while (tryToConnect)
            {
                Trace.WriteLine("Waiting to connect to " + ServerAddress.ToString());
                while (!Connect() & tryToConnect) { }
                tryToConnect = false;
                if (client.Connected) ListenForData();
            }
        }

        static bool Connect()
        {
            client = new TcpClient();

            try
            {
                client.Connect(ServerAddress);
            }
            catch
            {
                return false;
            }


            while (!client.Connected & tryToConnect) { }
            //Trace.WriteLine("Connected");

            //Trace.WriteLine("Client listening on: " + client.Client.LocalEndPoint.ToString());
            return true;
        }

        static void ListenForData()
        {
            Trace.WriteLine("Reading data.");
            Stream clientStream = client.GetStream();
            BinaryReader reader = new BinaryReader(clientStream);

            Queue<byte> rawData = new Queue<byte>();
            int length = 0;
            byte[] magicNumber = new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E };

            reader = new BinaryReader(clientStream);

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
                    Trace.WriteLine("Reading " + length.ToString() + " bytes.");
                    data = TelemetryData.Deserialize(rawData.ToArray());
                    rawData.Clear();
                }
                catch
                {
                    break;
                }
            }

            listening = false;
            client.GetStream().Close();
            client.Close();
        }
    }
}
