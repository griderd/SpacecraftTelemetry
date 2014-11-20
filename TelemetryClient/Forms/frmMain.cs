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
    public partial class frmMain : Form
    {
        KerbalDateTime universalTime;

        public frmMain()
        {
            InitializeComponent();
            universalTime = new KerbalDateTime(0);
        }

        private void btnBooster_Click(object sender, EventArgs e)
        {
            Program.booster.Show();
        }

        private void btnFlight_Click(object sender, EventArgs e)
        {
            Program.flight.Show();
        }

        private void btnRetro_Click(object sender, EventArgs e)
        {
            Program.retro.Show();
        }

        private void btnFDO_Click(object sender, EventArgs e)
        {
            Program.fdo.Show();
        }

        private void btnGuido_Click(object sender, EventArgs e)
        {
            Program.guido.Show();
        }

        private void btnSurgeon_Click(object sender, EventArgs e)
        {
            Program.surgeon.Show();
        }

        private void btnCapcom_Click(object sender, EventArgs e)
        {
            Program.capcom.Show();
        }

        private void btnEECOM_Click(object sender, EventArgs e)
        {
            Program.eecom.Show();
        }

        private void btnGNC_Click(object sender, EventArgs e)
        {
            Program.gnc.Show();
        }

        private void btnTELMU_Click(object sender, EventArgs e)
        {
            Program.telmu.Show();
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            Program.control.Show();
        }

        private void btnINCO_Click(object sender, EventArgs e)
        {
            Program.inco.Show();
        }

        private void btnProcedures_Click(object sender, EventArgs e)
        {
            Program.procedures.Show();
        }

        private void btnAFlight_Click(object sender, EventArgs e)
        {
            Program.aflight.Show();
        }

        private void btnFAO_Click(object sender, EventArgs e)
        {
            Program.fao.Show();
        }

        private void btnNetwork_Click(object sender, EventArgs e)
        {
            Program.network.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Program.flight.Show();
        }

        private void btnKASAHQ_Click(object sender, EventArgs e)
        {

        }

        private void btnFlightOps_Click(object sender, EventArgs e)
        {

        }

        private void btnPAO_Click(object sender, EventArgs e)
        {

        }

        private void btnDoD_Click(object sender, EventArgs e)
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

            if (universalTime.TotalSeconds != 0)
            {
                if (Program.data.universalTime != 0)
                {
                    universalTime = new KerbalDateTime(Program.data.universalTime);
                }
                else
                {
                    universalTime = new KerbalDateTime(universalTime.TotalSeconds + 0.1);
                }
            }
            else if (Program.data.universalTime != 0)
            {
                universalTime = new KerbalDateTime(Program.data.universalTime);
            }

            lblUniversalTime.Text = universalTime.ToString();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (!DesignMode) tmrUpdate.Enabled = true;
        }
    }
}
