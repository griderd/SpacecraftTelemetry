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
        Dictionary<string, string> files;
        string currentFile = "";
        int page = 0;
        bool endOfFile = false;

        enum ParseLocation
        {
            Root,
            PreStage,
            InStage
        }

        public frmProcedures()
        {
            InitializeComponent();
            files = new Dictionary<string, string>();
            files.Add("configParseErrors", "");
        }

        protected override void UpdateScreen()
        {
            base.UpdateScreen();

            if (currentFile != "")
            {
                terminal.Append(files[currentFile]);
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            files["configParseErrors"] = "";
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
                    int lineNumber = 0;
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
                                files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": Must have key and value pair.\n";
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
                                        files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'name' key cannot be used in this location.\n";
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
                                        files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'description' key cannot be used in this location.\n";
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
                                        files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'directory' key cannot be used in this location.\n";
                                    }
                                    break;

                                case "fuels":
                                    if (location == ParseLocation.InStage)
                                        fuels = value;  
                                    else
                                        files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'fuels' key cannot be used in this location.\n";
                                    break;
                            }
                        }
                        else if (line == "emptystage")
                        {
                            if (location == ParseLocation.Root)
                                Program.cfg.AddEmptyStage();
                            else
                                files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'emptystage' can only be used in root.\n";
                        }
                        else if (line == "stage")
                        {
                            if (location == ParseLocation.Root)
                                location = ParseLocation.PreStage;
                            else
                                files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": 'stage' structure can only be used in root.\n";
                        }
                        else if (line == "{")
                        {
                            if (location == ParseLocation.PreStage)
                                location = ParseLocation.InStage;
                            else
                                files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": '{' cannot be used in this location.\n";
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
                                files["configParseErrors"] = files["configParseErrors"] + "Line " + lineNumber.ToString() + ": '}' cannot be used in this location.\n";
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
                files.Add(file.Name, File.ReadAllText(file.FullName));
            }

            lstDocuments.Items.Clear();
            foreach (string filename in files.Keys)
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
