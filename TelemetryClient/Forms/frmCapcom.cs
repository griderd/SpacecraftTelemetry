﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmCapcom : TelemetryClient.frmControlTerminal
    {
        public frmCapcom()
        {
            InitializeComponent();
            Callsign = "CC";
            Nickname = "CAPCOM";
        }
    }
}
