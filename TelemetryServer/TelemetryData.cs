using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TelemetryServer
{
    [Serializable]
    public struct TelemetryData
    {
        #region Crew Info

        public CrewInfo[] crewData;

        #endregion

        #region Engine Data

        public double totalCurrentThrust, totalMaxThrust, totalIsp;
        public double[] currentThrust, maxThrust, isp;

        #endregion

        #region Orientation

        public double pitch, roll, heading;

        #endregion

        #region Resources

        public Dictionary<string, List<ResourceInfo>> partResources;
        public List<ResourceInfo> totalResources;

        #endregion

        #region Basic Data

        public double longitude, latitude;
        public double acceleration, angularMomentum, angularVelocity, orbitalVelocity, surfaceVelocity;
        public double atmosphericDensity, geeForce, heightFromTerrain, missionTime, orbitSpeed,
            surfaceSpeed, staticPressure, verticalSpeed, terrainAltitude, altitude, universalTime;
        public double apoapsis, periapsis, timeToApoapsis, timeToPeriapsis, longitudeOfAscendingNode,
            inclination, eccentricity, meanAnomaly, period, trueAnomaly;
        public string vesselName;
        public int currentStage;

        #endregion

        #region Network Data

        public string[] clientEndpoints;
        public string serverEndpoint;

        #endregion

        public static byte[] Serialize(TelemetryData data)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memory, data);
                return memory.ToArray();
            }
        }

        public static TelemetryData Deserialize(byte[] data)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                memory.Write(data, 0, data.Length);
                memory.Seek(0, SeekOrigin.Begin);
                return (TelemetryData)formatter.Deserialize(memory);
            }
        }
    }

    [Serializable]
    public struct CrewInfo
    {
        public string crewName;
        public double whee;
        public double panic;

        public CrewInfo(string name, double whee, double panic)
            : this()
        {
            crewName = name;
            this.whee = whee;
            this.panic = panic;

        }
    }

    [Serializable]
    public struct ResourceInfo
    {
        public string resourceName;
        public double currentAmount;
        public double maxAmount;
        public double density;
        public int stage;
    }
}
