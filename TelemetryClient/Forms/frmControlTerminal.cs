using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmControlTerminal : Form
    {
        protected StringBuilder terminal;

        public frmControlTerminal()
        {
            InitializeComponent();
        }

        private void frmControlTerminal_Load(object sender, EventArgs e)
        {
            if (!DesignMode) tmrUpdate.Enabled = true;
            terminal = new StringBuilder();
        }

        public void ResetGoNoGo()
        {
            btnGo.ResetState();
            btnNoGo.ResetState();
            btnGo.Enabled = true;
            btnNoGo.Enabled = true;
        }

        private void btnNoGo_Load(object sender, EventArgs e)
        {
            btnNoGo.Text = "NO GO";
        }

        private void btnNoGo_Click(object sender, EventArgs e)
        {
            Program.SetGo(this, CheckTristate.NoGo);
            btnGo.Enabled = false;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Program.SetGo(this, CheckTristate.Go);
            btnNoGo.Enabled = false;
        }

        private void frmControlTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DesignMode)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void btnGo_Load(object sender, EventArgs e)
        {

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            KerbalTimespan missionTime;
            if (Program.countdownTime < 0)
                missionTime = new KerbalTimespan(Program.countdownTime);
            else
                missionTime = new KerbalTimespan(Program.data.missionTime);

            lblMissionTime.Text = missionTime.ToString();

            UpdateScreen();
            UpdateTerminal();
        }

        protected virtual void UpdateScreen()
        {
        }

        protected virtual void UpdateTerminal()
        {
            lblTerminal.Text = terminal.ToString();
            terminal.Clear();
        }

        protected void AppendToTerminal(string label, string value)
        {
            terminal.Append(label);
            terminal.Append(": ");
            terminal.AppendLine(value);
        }

        protected void AppendToTerminal(string label, int value)
        {
            terminal.Append(label);
            terminal.Append(": ");
            terminal.Append(value);
            terminal.AppendLine();
        }

        protected void AppendToTerminal(string label, double value)
        {
            terminal.Append(label);
            terminal.Append(": ");
            terminal.AppendLine(value.ToString("F2"));
        }

        protected string GenerateScale(double percentage)
        {
            StringBuilder scale = new StringBuilder();

            double perc = Math.Round(percentage, 2);

            int bars = (int)Math.Round(perc / 5.0);
            double partial = perc - (bars * 5);
            bool includePartial = (partial > 0) & (partial < 5);

            scale.Append('[');
            scale.Append(new string('\u2588', bars));
            if (includePartial) scale.Append('\u258C');
            scale.Append(new string(' ', 20 - bars - (includePartial ? 1 : 0)));
            scale.Append(']');

            return scale.ToString();
        }

        protected void AppendResource(string resourceName, string alias)
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
    }
}
