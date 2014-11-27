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

        public static Client client;

        public static Dictionary<string, string> documents;

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
            client = new Client(ServerAddress);
            client.DataAvailable += new EventHandler<DataAvailableEventArgs>(client_DataAvailable);

            documents = new Dictionary<string, string>();

            stationsGo = new Dictionary<frmControlTerminal, CheckTristate>();

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
            client.StartConnecting();
        }

        public static void EndCollectData()
        {
            client.Disconnect();
        }

        public static void client_DataAvailable(object sender, DataAvailableEventArgs e)
        {
            if (e.Data.Protocol == Protocols.TelemetryData)
            {
                data = (TelemetryData)e.Data.GetData();
            }
            else
            {
                // TODO: Warn that the protocol is incorrect.
            }
        }
    }
}
