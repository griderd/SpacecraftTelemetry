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
        public frmNetwork()
        {
            InitializeComponent();
        }

        private void frmNetwork_Load(object sender, EventArgs e)
        {

        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            AppendToTerminal("Has Signal", Program.Connected.ToString());
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
}
