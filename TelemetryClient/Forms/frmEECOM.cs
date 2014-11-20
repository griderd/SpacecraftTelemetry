using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmEECOM : TelemetryClient.frmControlTerminal
    {
        public frmEECOM()
        {
            InitializeComponent();
        }

        protected override void UpdateScreen()
        {
            AppendResource("ElectricCharge", "Power");
            terminal.AppendLine();
            AppendResource("Oxygen", "Oxygen");
            terminal.AppendLine();
            AppendResource("Hydrogen", "Hydrogen");
            terminal.AppendLine();
            AppendResource("CarbonDioxide", "Carbon Dioxide");
            terminal.AppendLine();
            AppendResource("Food", "Food");
            terminal.AppendLine();
            AppendResource("Water", "Water");
            terminal.AppendLine();
            AppendResource("Waste", "Waste");
            terminal.AppendLine();
            AppendResource("WasteWater", "Waste Water");
            terminal.AppendLine();
        }
    }
}
