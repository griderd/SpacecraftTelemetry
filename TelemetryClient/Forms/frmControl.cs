﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmControl : TelemetryClient.frmControlTerminal
    {
        public frmControl()
        {
            InitializeComponent();
            Callsign = "CONTROL";
            Nickname = "CONTROL";
        }
    }
}
