using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmFlightDirector : TelemetryClient.frmControlTerminal
    {
        public frmFlightDirector()
        {
            InitializeComponent();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Program.ResetGoNoGo();
        }

        protected override void UpdateScreen()
        {
            UpdateLights();

            AppendToTerminal("Stage", Program.data.currentStage);
            AppendToTerminal("Altitude", Program.data.altitude);
            AppendToTerminal("Latitude", Program.data.latitude);
            AppendToTerminal("Longitude", Program.data.longitude);
            AppendToTerminal("Velocity", Program.data.orbitalVelocity);
            AppendToTerminal("Pitch", Program.data.pitch);
            AppendToTerminal("Roll", Program.data.roll);
            AppendToTerminal("Heading", Program.data.heading);

            if ((Program.runCountdown) & (Program.countdownTime <= 0))
                Program.countdownTime += 0.1;
        }

        void UpdateLights()
        {
            if (Program.stationsGo[Program.booster] == CheckTristate.Go) lblBooster.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.booster] == CheckTristate.NoGo) lblBooster.BackColor = Color.Red;
            else lblBooster.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.retro] == CheckTristate.Go) lblRetro.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.retro] == CheckTristate.NoGo) lblRetro.BackColor = Color.Red;
            else lblRetro.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.fdo] == CheckTristate.Go) lblFDO.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.fdo] == CheckTristate.NoGo) lblFDO.BackColor = Color.Red;
            else lblFDO.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.guido] == CheckTristate.Go) lblGuido.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.guido] == CheckTristate.NoGo) lblGuido.BackColor = Color.Red;
            else lblGuido.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.surgeon] == CheckTristate.Go) lblSurgeon.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.surgeon] == CheckTristate.NoGo) lblSurgeon.BackColor = Color.Red;
            else lblSurgeon.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.capcom] == CheckTristate.Go) lblCapcom.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.capcom] == CheckTristate.NoGo) lblCapcom.BackColor = Color.Red;
            else lblCapcom.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.eecom] == CheckTristate.Go) lblEECOM.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.eecom] == CheckTristate.NoGo) lblEECOM.BackColor = Color.Red;
            else lblEECOM.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.gnc] == CheckTristate.Go) lblGNC.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.gnc] == CheckTristate.NoGo) lblGNC.BackColor = Color.Red;
            else lblGNC.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.telmu] == CheckTristate.Go) lblTelmu.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.telmu] == CheckTristate.NoGo) lblTelmu.BackColor = Color.Red;
            else lblTelmu.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.inco] == CheckTristate.Go) lblINCO.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.inco] == CheckTristate.NoGo) lblINCO.BackColor = Color.Red;
            else lblINCO.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.procedures] == CheckTristate.Go) lblProcedures.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.procedures] == CheckTristate.NoGo) lblProcedures.BackColor = Color.Red;
            else lblProcedures.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.fao] == CheckTristate.Go) lblFAO.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.fao] == CheckTristate.NoGo) lblFAO.BackColor = Color.Red;
            else lblFAO.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.network] == CheckTristate.Go) lblNetwork.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.network] == CheckTristate.NoGo) lblNetwork.BackColor = Color.Red;
            else lblNetwork.BackColor = Color.FromArgb(64, 64, 0);

            if (Program.stationsGo[Program.control] == CheckTristate.Go) lblControl.BackColor = Color.Lime;
            else if (Program.stationsGo[Program.control] == CheckTristate.NoGo) lblControl.BackColor = Color.Red;
            else lblControl.BackColor = Color.FromArgb(64, 64, 0);
        }

        private void btnCountdownStart_Click(object sender, EventArgs e)
        {
            Program.runCountdown = true;
            Program.countdownTime = (double)nudTime.Value * -60;
        }

        private void btnCountdownHold_Click(object sender, EventArgs e)
        {
            if (Program.countdownTime < 0)
                Program.runCountdown = !Program.runCountdown;
        }

        private void btnCountdownReset_Click(object sender, EventArgs e)
        {
            Program.countdownTime = 1;
        }
    }
}
