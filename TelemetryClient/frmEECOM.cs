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

        string GenerateScale(double percentage)
        {
            StringBuilder scale = new StringBuilder();

            double perc = Math.Round(percentage);

            int bars = (int)(perc / 5.0);
            scale.Append('[');
            scale.Append(new string('\u2592', bars));
            scale.Append(new string(' ', 20 - bars));
            scale.Append(']');

            return scale.ToString();
        }

        void AppendResource(string resourceName, string alias)
        {
            double amount = Program.GetResourceInfo(resourceName).currentAmount;
            double max = Program.GetResourceInfo(resourceName).maxAmount;
            double percent = max > 0.0 ? amount / max : 0.0;

            terminal.Append(alias);
            terminal.Append(": ");
            terminal.Append(amount.ToString("F2"));
            terminal.Append('/');
            terminal.Append(max.ToString("F2"));
            terminal.Append(" (");
            terminal.Append(percent.ToString("P"));
            terminal.AppendLine(")");
            terminal.AppendLine(GenerateScale(percent * 100));
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
        }
    }
}
