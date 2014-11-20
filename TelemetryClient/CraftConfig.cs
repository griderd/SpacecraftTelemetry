using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TelemetryClient
{
    public class CraftConfig
    {
        List<CraftStage> stages;

        public CraftStage[] Stages { get { return stages.ToArray(); } }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DirectoryInfo DocumentDirectory { get; private set; }

        public CraftConfig(string configName, string configDescription, string directory)
        {
            stages = new List<CraftStage>();
            Name = configName;
            Description = configDescription;
            DocumentDirectory = new DirectoryInfo(directory);
        }

        public void AddEmptyStage()
        {
            stages.Add(new CraftStage());
        }

        public void AddStage(string name, string[] fuels)
        {
            stages.Add(new CraftStage(name, fuels));
        }
    }

    public struct CraftStage
    {
        public string Name { get; private set; }
        public string[] Fuels { get; private set; }

        public CraftStage(string name, string[] fuels)
            : this()
        {
            Name = name;
            Fuels = fuels;
        }
    }
}
