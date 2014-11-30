using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient.Forms
{
    public partial class frmPAO : TelemetryClient.frmControlTerminal
    {
        StringBuilder cockpitTranscript;
        StringBuilder groundTranscipt;
        StringBuilder airGroundTranscript;
        StringBuilder paoCommentary;
        StringBuilder allEventsTranscript;

        public frmPAO()
        {
            InitializeComponent();
            Program.FlightEvent += new EventHandler<FlightEventArgs>(Program_FlightEvent);

            cockpitTranscript = new StringBuilder();
            groundTranscipt = new StringBuilder();
            airGroundTranscript = new StringBuilder();
            paoCommentary = new StringBuilder();
            allEventsTranscript = new StringBuilder();

            Callsign = "PAO";
            Nickname = "PAO";
        }

        private void Program_FlightEvent(object sender, FlightEventArgs e)
        {
            AppendToTranscripts(e, allEventsTranscript);
        }

        private void txtCommentary_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            FlightEventArgs data = new FlightEventArgs("PAO", txtCommentary.Text);
            AppendToTranscripts(data, allEventsTranscript, paoCommentary);

            txtCommentary.Text = "Write Commentary Here";
        }

        private void txtCommentary_Click(object sender, EventArgs e)
        {
            if (txtCommentary.Text == "Write Commentary Here")
                txtCommentary.Text = "";
        }

        private void AppendToTranscripts(FlightEventArgs eventArgs, params StringBuilder[] transcripts)
        {
            foreach (StringBuilder transcript in transcripts)
            {
                transcript.Append('(');
                transcript.Append(Program.GetTime());
                transcript.Append(") ");
                transcript.Append(eventArgs.Sender);
                transcript.Append("\t");
                transcript.AppendLine(eventArgs.EventString);
                transcript.AppendLine();
            }
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            txtFlightEvents.Text = allEventsTranscript.ToString();
            txtFlightEvents.Select(txtFlightEvents.Text.Length, 0);
            txtFlightEvents.ScrollToCaret();
        }
    }
}
