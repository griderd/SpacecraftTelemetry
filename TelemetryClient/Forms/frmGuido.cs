﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmGuido : TelemetryClient.frmControlTerminal
    {
        public frmGuido()
        {
            InitializeComponent();
            Callsign = "GUIDO";
            Nickname = "GUIDANCE";
        }
    }
}
