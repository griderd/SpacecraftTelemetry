using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmGNC : TelemetryClient.frmControlTerminal
    {
        public frmGNC()
        {
            InitializeComponent();
            Callsign = "GNC";
            Nickname = "GNC";
        }
    }
}
