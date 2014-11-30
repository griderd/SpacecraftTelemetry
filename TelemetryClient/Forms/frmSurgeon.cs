using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TelemetryServer;

namespace TelemetryClient
{
    public partial class frmSurgeon : TelemetryClient.frmControlTerminal
    {
        public frmSurgeon()
        {
            InitializeComponent();
            Callsign = "SURGEON";
            Nickname = "SURGEON";
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            if (Program.data.crewData == null) return;
            
            foreach (CrewInfo kerbal in Program.data.crewData)
            {
                terminal.AppendLine(kerbal.crewName);
                terminal.Append("Panic: ");
                terminal.Append(kerbal.panic * 100);
                terminal.Append(" ");
                terminal.AppendLine(GenerateScale(kerbal.panic * 100));
                terminal.Append("Whee: ");
                terminal.Append(kerbal.whee * 100);
                terminal.Append(" ");
                terminal.AppendLine(GenerateScale(kerbal.whee * 100));
                terminal.AppendLine();
            }
        }
    }
}
