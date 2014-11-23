using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmFDO : TelemetryClient.frmControlTerminal
    {
        public frmFDO()
        {
            InitializeComponent();
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            double semiMajorAxis = (Program.data.apoapsis + Program.data.periapsis) / 2;
            double semiLatusRectum = semiMajorAxis * (1 - Math.Pow(Program.data.eccentricity, 2));

            AppendToTerminal("Ap", Program.data.apoapsis);
            AppendToTerminal("Pe", Program.data.periapsis);
            AppendToTerminal("e", Program.data.eccentricity);
            AppendToTerminal("a", semiMajorAxis);
            AppendToTerminal("p", semiLatusRectum);
            AppendToTerminal("v", Program.data.trueAnomaly);
            AppendToTerminal("i", Program.data.inclination);
            AppendToTerminal("Ω", Program.data.longitudeOfAscendingNode);
        }
    }
}
