using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class frmFAO : TelemetryClient.frmControlTerminal
    {
        string currentFile = "";
        List<string> pages;
        int page = 0;
        bool endOfFile = false;

        public frmFAO()
        {
            InitializeComponent();
            Callsign = "FAO";
            Nickname = "FAO";
        }

        private void frmFAO_Load(object sender, EventArgs e)
        {
            pages = new List<string>();
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            List<string> items = new List<string>();
            foreach (object item in lstDocuments.Items)
            {
                items.Add((string)item);
            }

            string[] keys = new string[Program.documents.Keys.Count];
            Program.documents.Keys.CopyTo(keys, 0);
            if (GSLib.Collections.Helpers.CompareArrays<string>(items.ToArray(), keys) != 0)
            {
                lstDocuments.Items.Clear();
                foreach (string filename in Program.documents.Keys)
                {
                    lstDocuments.Items.Add(filename);
                }

                currentFile = "";
            }
            else
            {
                // TODO: display by page
                if (currentFile != "")
                {
                    terminal.Append(Program.documents[currentFile]);
                }
                else
                {
                    terminal.Append("Select a file to view.");
                }
            }
        }

        private void lstDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = 0;
            currentFile = (string)lstDocuments.Items[lstDocuments.SelectedIndex];
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (page > 0)
                page--;
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (!endOfFile)
                page++;
        }
    }
}
