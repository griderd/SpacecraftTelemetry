using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TelemetryClient
{    
    public partial class frmInco : TelemetryClient.frmControlTerminal
    {
        StreamWriter writer;
        FileStream stream;
        string filename;

        public frmInco()
        {
            InitializeComponent();

            filename = "N/A";
            Callsign = "INCO";
            Nickname = "INCO";
        }

        private void chkRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecord.Checked)
            {
                dlgSave.DefaultExt = "csv";
                if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    stream = new FileStream(dlgSave.FileName, FileMode.Create);
                    writer = new StreamWriter(stream);
                    filename = dlgSave.FileName;

                    WriteHeader();
                }
                else
                {
                    chkRecord.Checked = false;
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to stop recording?", "ICNO", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    writer.Close();
                    stream.Close();

                    writer.Dispose();
                    stream.Dispose();

                    filename = "N/A";
                }
                else
                {
                    chkRecord.Checked = true;
                }
            }
        }

        private void WriteHeader()
        {
            writer.Write("UT,missionTime,vesselName,currentStage,pitch,roll,heading,longitude,latitude,");
            writer.Write("acceleration,angularMomentum,angularVelocity,orbitalVelocity,surfaceVelocity,");
            writer.Write("atmosphericDensity,geeForce,heightFromTerrain,");
            writer.Write("staticPressure,verticalSpeed,terrainAltitude,altitude,");
            writer.Write("apoapsis,periapsis,timeToApoapsis,timeToPeriapsis,longitudeOfAscendingNode,");
            writer.WriteLine("inclination,eccentricity,meanAnomaly,period,trueAnomaly");
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            AppendToTerminal("Recording", chkRecord.Checked.ToString());
            AppendToTerminal("File Path", filename);

            if (chkRecord.Checked & writer != null)
            {
                writer.Write(Program.data.universalTime); writer.Write(',');
                writer.Write(Program.data.missionTime); writer.Write(',');
                writer.Write(Program.data.vesselName); writer.Write(',');
                writer.Write(Program.data.currentStage); writer.Write(',');
                writer.Write(Program.data.pitch); writer.Write(',');
                writer.Write(Program.data.roll); writer.Write(',');
                writer.Write(Program.data.heading); writer.Write(',');
                writer.Write(Program.data.longitude); writer.Write(',');
                writer.Write(Program.data.latitude); writer.Write(',');
                writer.Write(Program.data.acceleration); writer.Write(',');
                writer.Write(Program.data.angularMomentum); writer.Write(',');
                writer.Write(Program.data.angularVelocity); writer.Write(',');
                writer.Write(Program.data.orbitalVelocity); writer.Write(',');
                writer.Write(Program.data.surfaceVelocity); writer.Write(',');
                writer.Write(Program.data.atmosphericDensity); writer.Write(',');
                writer.Write(Program.data.geeForce); writer.Write(',');
                writer.Write(Program.data.heightFromTerrain); writer.Write(',');
                writer.Write(Program.data.staticPressure); writer.Write(',');
                writer.Write(Program.data.verticalSpeed); writer.Write(',');
                writer.Write(Program.data.terrainAltitude); writer.Write(',');
                writer.Write(Program.data.altitude); writer.Write(',');
                writer.Write(Program.data.apoapsis); writer.Write(',');
                writer.Write(Program.data.periapsis); writer.Write(',');
                writer.Write(Program.data.timeToApoapsis); writer.Write(',');
                writer.Write(Program.data.timeToPeriapsis); writer.Write(',');
                writer.Write(Program.data.longitudeOfAscendingNode); writer.Write(',');
                writer.Write(Program.data.inclination); writer.Write(',');
                writer.Write(Program.data.eccentricity); writer.Write(',');
                writer.Write(Program.data.meanAnomaly); writer.Write(',');
                writer.Write(Program.data.period); writer.Write(',');
                writer.WriteLine(Program.data.trueAnomaly);
            }

            AppendToTerminal("UT", Program.data.universalTime);
            AppendToTerminal("Mission Time", Program.data.missionTime);
            AppendToTerminal("Vessel Name", Program.data.vesselName);
            AppendToTerminal("Current Stage", Program.data.currentStage);
            AppendToTerminal("Pitch", Program.data.pitch);
            AppendToTerminal("Roll", Program.data.roll);
            AppendToTerminal("Heading", Program.data.heading);
            AppendToTerminal("Longitude", Program.data.longitude);
            AppendToTerminal("Latitude", Program.data.latitude);
            AppendToTerminal("Acceleration", Program.data.acceleration);
            AppendToTerminal("Angular Momentum", Program.data.angularMomentum);
            AppendToTerminal("Angular Velocity", Program.data.angularVelocity);
            AppendToTerminal("Orbital Velocity", Program.data.orbitalVelocity);
            AppendToTerminal("Surface Velocity", Program.data.surfaceVelocity);
            AppendToTerminal("Atmospheric Density", Program.data.atmosphericDensity);
            AppendToTerminal("Gee Force", Program.data.geeForce);
            AppendToTerminal("Height From Terrain", Program.data.heightFromTerrain);
            AppendToTerminal("Static Pressure", Program.data.staticPressure);
            AppendToTerminal("Vertical Speed", Program.data.verticalSpeed);
            AppendToTerminal("Terrain Altitude", Program.data.terrainAltitude);
            AppendToTerminal("Altitude", Program.data.altitude);
            AppendToTerminal("Apoapsis", Program.data.apoapsis);
            AppendToTerminal("Periapsis", Program.data.periapsis);
            AppendToTerminal("Time to Apoapsis", Program.data.timeToApoapsis);
            AppendToTerminal("Time to Periapsis", Program.data.timeToPeriapsis);
            AppendToTerminal("Longitude of Ascending Node", Program.data.longitudeOfAscendingNode);
            AppendToTerminal("Inclination", Program.data.inclination);
            AppendToTerminal("Eccentricity", Program.data.eccentricity);
            AppendToTerminal("Mean Anomaly", Program.data.meanAnomaly);
            AppendToTerminal("Period", Program.data.period);
            AppendToTerminal("True Anomaly", Program.data.trueAnomaly);

        }
    }
}
