using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmBooster : TelemetryClient.frmControlTerminal
    {
        double initialVelocity;

        public frmBooster()
        {
            InitializeComponent();
        }

        protected override void UpdateScreen()
        {
            double currentThrust;
            double maxThrust;
            double throttle = 0.0f;

            double velocity, deltaVelocity = 0;

            velocity = Program.data.orbitalVelocity;
            deltaVelocity = velocity - initialVelocity;

            currentThrust = Program.data.totalCurrentThrust;
            maxThrust = Program.data.totalMaxThrust;

            AppendToTerminal("Stage", Program.data.currentStage);
            AppendToTerminal("Total Thrust", currentThrust);
            AppendToTerminal("Total Max Thrust", maxThrust);

            throttle = currentThrust / maxThrust;

            AppendToTerminal("Throttle ", throttle.ToString("p"));

            AppendToTerminal("Acceleration", Program.data.acceleration);
            AppendToTerminal("Orbital Velocity", Program.data.orbitalVelocity);
            AppendToTerminal("ΔV", deltaVelocity);
            AppendToTerminal("Altitude", Program.data.altitude);

            if (Program.cfg != null)
            {
                foreach (string resource in Program.cfg.Stages[Program.data.currentStage].Fuels)
                {
                    AppendResource(resource, resource);
                }
            }
        }

        private void frmBooster_Load(object sender, EventArgs e)
        {
            initialVelocity = 0;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            btnAbort.BackColor = Color.Red;

            // TODO: Send abort signal
        }

        private void btnResetDeltaV_Click(object sender, EventArgs e)
        {
            initialVelocity = Program.data.orbitalVelocity;
        }

    }
}
