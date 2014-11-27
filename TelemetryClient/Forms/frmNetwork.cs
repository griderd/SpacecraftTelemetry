using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmNetwork : TelemetryClient.frmControlTerminal
    {
        string errorMessage;

        public frmNetwork()
        {
            InitializeComponent();
        }

        private void frmNetwork_Load(object sender, EventArgs e)
        {
            errorMessage = "";
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            if (Program.client.AwaitingConnection)
            {
                btnConnect.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = true;
                btnCancel.Enabled = false;
            }

            if (errorMessage != "")
            {
                terminal.AppendLine(errorMessage);
            }
            else
            {
                AppendToTerminal("Awaiting Connection", Program.client.AwaitingConnection.ToString());
                AppendToTerminal("Connected To Server", Program.client.HasConnection.ToString());
                AppendToTerminal("Listening For Data", Program.client.Listening.ToString());
                AppendToTerminal("Spacecraft Server", Program.data.serverEndpoint);
                if (Program.data.clientEndpoints != null)
                {
                    for (int i = 0; i < Program.data.clientEndpoints.Length; i++)
                    {
                        AppendToTerminal("Client " + (i + 1).ToString(), Program.data.clientEndpoints[i]);
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!Program.client.HasConnection)
            {
                int port;
                System.Net.IPAddress address;
                if ((int.TryParse(txtServerPort.Text, out port)) & (System.Net.IPAddress.TryParse(txtAddress.Text, out address)))
                {
                    Program.ServerAddress = new System.Net.IPEndPoint(address, port);
                    Program.BeginCollectData();
                }
                else
                {
                    errorMessage = "ERROR! Could not parse IP address or port.";
                }
            }
            else
            {
                Program.EndCollectData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Program.client.StopConnecting();
        }
    }
}
