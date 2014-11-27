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
    public partial class frmProcedures : TelemetryClient.frmControlTerminal
    {
        string currentFile = "";
        int page = 0;
        bool endOfFile = false;

        int lineNumber = 0;

        enum ParseLocation
        {
            Root,
            PreStage,
            InStage
        }

        public frmProcedures()
        {
            InitializeComponent();
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            if (currentFile != "")
            {
                terminal.Append(Program.documents[currentFile]);
            }
        }

        private void AddParseError(string message)
        {
            Program.documents["configParseErrors"] = Program.documents["configParseErrors"] + "Line " + lineNumber.ToString() + ": " + message + "\n";
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            Program.documents.Clear();

            Program.documents["configParseErrors"] = "";
            string name = "";
            string description = "";
            string directory = "";
            string stageName = "";
            string fuels = "";

            ParseLocation location = ParseLocation.Root;

            StreamReader reader;
            if (dlgOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reader = new StreamReader(dlgOpenFile.OpenFile());

                for (; ; )
                {
                    lineNumber = 0;
                    try
                    {
                        lineNumber++;
                        string line = reader.ReadLine().Trim();
                        if (line.StartsWith("//")) continue;
                        if (line.Contains("="))
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length != 2)
                            {
                                AddParseError("Must have key and value pair.");
                            }
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (key)
                            {
                                case "name":
                                    if (location == ParseLocation.Root)
                                    {
                                        name = value;
                                        if ((description != "") & (directory != ""))
                                        {
                                            Program.cfg = new CraftConfig(name, description, directory);
                                        }
                                    }
                                    else if (location == ParseLocation.InStage)
                                    {
                                        stageName = value;
                                    }
                                    else
                                    {
                                        AddParseError("'name' key cannot be used in this location.");
                                    }
                                    break;

                                case "description":
                                    if (location == ParseLocation.Root)
                                    {
                                        description = value;
                                        if ((name != "") & (directory != ""))
                                        {
                                            Program.cfg = new CraftConfig(name, description, directory);
                                        }
                                    }
                                    else
                                    {
                                        AddParseError("'description' key cannot be used in this location.");
                                    }
                                    break;

                                case "directory":
                                    if (location == ParseLocation.Root)
                                    {
                                        directory = value;
                                        if ((description != "") & (name != ""))
                                        {
                                            Program.cfg = new CraftConfig(name, description, directory);
                                        }
                                    }
                                    else
                                    {
                                        AddParseError("'directory' key cannot be used in this location.");
                                    }
                                    break;

                                case "fuels":
                                    if (location == ParseLocation.InStage)
                                        fuels = value;
                                    else
                                        AddParseError("'fuels' key cannot be used in this location.");
                                    break;
                            }
                        }
                        else if (line == "emptystage")
                        {
                            if (location == ParseLocation.Root)
                                Program.cfg.AddEmptyStage();
                            else
                                AddParseError("'emptystage' can only be used in root.");
                        }
                        else if (line == "stage")
                        {
                            if (location == ParseLocation.Root)
                                location = ParseLocation.PreStage;
                            else
                                AddParseError("'stage' structure can only be used in root.");
                        }
                        else if (line == "{")
                        {
                            if (location == ParseLocation.PreStage)
                                location = ParseLocation.InStage;
                            else
                                AddParseError("'{' cannot be used in this location.");
                        }
                        else if (line == "}")
                        {
                            if (location == ParseLocation.InStage)
                            {
                                if (stageName != "" & fuels != "")
                                {
                                    Program.cfg.AddStage(stageName, fuels.Split(','));
                                }
                                location = ParseLocation.Root;
                            }
                            else
                            {
                                AddParseError("'}' cannot be used in this location.");
                            }
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            FileInfo[] fileList = Program.cfg.DocumentDirectory.GetFiles();
            foreach (FileInfo file in fileList)
            {
                try
                {
                    Program.documents.Add(file.Name, File.ReadAllText(file.FullName));
                }
                catch
                {
                    // Do nothing for now
                }
            }

            lstDocuments.Items.Clear();
            foreach (string filename in Program.documents.Keys)
            {
                lstDocuments.Items.Add(filename);
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
