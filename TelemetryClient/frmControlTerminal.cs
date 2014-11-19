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
            double missionTimeSeconds = Program.data.missionTime;
            KerbalTimespan missionTime = new KerbalTimespan(Program.data.missionTime);

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
    }
}
