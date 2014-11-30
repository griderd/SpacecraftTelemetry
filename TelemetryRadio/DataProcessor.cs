using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using KSP;
using UnityEngine;
using TelemetryServer;

namespace TelemetryRadio
{
    class DataProcessor
    {
        #region Fields

        Vessel vessel;
        Server server;

        #region Crew/Biomed

        ProtoCrewMember[] crew;
        kerbalExpressionSystem[] biomed;

        List<CrewInfo> crewData;

        #endregion

        #region Engines

        double totalCurrentThrust;
        double totalMaxThrust;
        double totalIsp;
        List<double> currentThrust;
        List<double> maxThrust;
        List<double> isp; 

        #endregion

        #region Orientation

        Quaternion rotationSurface;
        Quaternion rotationVesselSurface;

        double pitch;
        double roll;
        double heading;

        #endregion

        #region Resources

        Dictionary<string, List<ResourceInfo>> partResources;
        Dictionary<string, ResourceInfo> totalResources;

        #endregion

        #endregion

        #region Constructor
        public DataProcessor(Vessel vessel, Server server)
        {
            this.vessel = vessel;
            this.server = server;

            totalResources = new Dictionary<string, ResourceInfo>();
            partResources = new Dictionary<string, List<ResourceInfo>>();
            crewData = new List<CrewInfo>();
        }
        #endregion

        public TelemetryData GetTelemetryData()
        {
#if DEBUG
            TelemetryRadioLogger.Print("Translating telemetry data.");
#endif
            return new TelemetryData
            {
                crewData = this.crewData.ToArray(),
                totalCurrentThrust = TotalCurrentThrust,
                totalMaxThrust = TotalMaxThrust,
                totalIsp = TotalIsp,
                currentThrust = this.currentThrust.ToArray(),
                maxThrust = this.maxThrust.ToArray(),
                isp = this.isp.ToArray(),
                pitch = Pitch,
                roll = Roll,
                heading = Heading,
                longitude = Longitude,
                latitude = Latitude,
                acceleration = Acceleration,
                angularMomentum = AngularMomentum,
                angularVelocity = AngularVelocity,
                orbitalVelocity = OrbitalVelocity,
                surfaceVelocity = SurfaceVelocity,
                atmosphericDensity = AtmosphericDensity,
                geeForce = GeeForce,
                heightFromTerrain = HeightFromTerrain,
                missionTime = MissionTime,
                orbitSpeed = OrbitSpeed,
                surfaceSpeed = SurfaceSpeed,
                staticPressure = StaticPressure,
                verticalSpeed = VerticalSpeed,
                terrainAltitude = TerrainAltitude,
                clientEndpoints = ClientEndpoints,
                serverEndpoint = ServerEndpoint,
                currentStage = CurrentStage,
                altitude = Altitude,
                vesselName = VesselName,
                partResources = this.partResources,
                totalResources = new List<ResourceInfo>(this.totalResources.Values.ToArray()),
                universalTime = UniversalTime,
                apoapsis = Apoapsis,
                periapsis = Periapsis,
                timeToApoapsis = TimeToApoapsis,
                timeToPeriapsis = TimeToPeriapsis,
                longitudeOfAscendingNode = LongitudeOfAscendingNode,
                inclination = Inclination,
                eccentricity = Eccentricity,
                meanAnomaly = MeanAnomaly,
                period = Period,
                trueAnomaly = TrueAnomaly
            };
        }

        #region Helpers
        double Format(double value, int places = 2)
        {
            return Math.Round(value, places);
        }

        #region Update Functions
        public void Update()
        {
            UpdateCrew();
            UpdateEngines();
            UpdateOrientation();
            UpdateResources();
        }

        void UpdateCrew()
        {
#if DEBUG
            TelemetryRadioLogger.Print("Updating crew data.");
#endif
            crew = vessel.GetVesselCrew().ToArray();
            biomed = new kerbalExpressionSystem[crew.Length];
            crewData = new List<CrewInfo>();

            for (int i = 0; i < biomed.Length; i++)
            {
                biomed[i] = crew[i].KerbalRef != null ? crew[i].KerbalRef.GetComponent<kerbalExpressionSystem>() : null;
                if (crew[i].KerbalRef != null)
                {
                    crewData.Add(new CrewInfo(crew[i].KerbalRef.crewMemberName, biomed[i].wheeLevel, biomed[i].panicLevel, crew[i].seatIdx));
                }
            }
        }

        void UpdateEngines()
        {
#if DEBUG
            TelemetryRadioLogger.Print("Updating engine data.");
#endif
            currentThrust = new List<double>();
            maxThrust = new List<double>();
            isp = new List<double>();
            totalCurrentThrust = 0.0f;
            totalMaxThrust = 0.0f;
            totalIsp = 0.0f;

            foreach (Part part in vessel.parts)
            {
                foreach (PartModule engine in part.Modules)
                {
                    if ((engine.moduleName == "ModuleEngines") |
                        (engine.moduleName == "ModuleEnginesFX"))
                    {
                        double ct = GetCurrentThrust(engine);
                        double mt = GetMaximumThrust(engine);
                        double i = GetRealIsp(engine);
                        
                        currentThrust.Add(ct);
                        maxThrust.Add(mt);
                        isp.Add(i);

                        totalCurrentThrust += ct;
                        totalMaxThrust += mt;
                        totalIsp += i;
                    }
                }
            }
        }

        void UpdateOrientation()
        {
#if DEBUG
            TelemetryRadioLogger.Print("Updating vessel orientation data.");
#endif
            Vector3 CoM = vessel.findWorldCenterOfMass();
            Vector3 up = (CoM - vessel.mainBody.position).normalized;
            Vector3d north = Vector3d.Exclude(up, (vessel.mainBody.position + vessel.mainBody.transform.up * (float)vessel.mainBody.Radius) - CoM).normalized;
            
            rotationSurface = Quaternion.LookRotation(north, up);
            rotationVesselSurface = Quaternion.Inverse(Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(vessel.GetTransform().rotation) * rotationSurface);

            heading = rotationVesselSurface.eulerAngles.y;
            pitch = (rotationVesselSurface.eulerAngles.x > 180) ? (360.0 - rotationVesselSurface.eulerAngles.x) : -rotationVesselSurface.eulerAngles.x;
            roll = (rotationVesselSurface.eulerAngles.z > 180) ? (rotationVesselSurface.eulerAngles.z - 360.0) : rotationVesselSurface.eulerAngles.z;
        }

        string GetUniquePartName(Part part)
        {
            int index = 0;
            string name = part.partName;

            if (partResources.ContainsKey(name))
            {
                do
                {
                    index++;
                }
                while (partResources.ContainsKey(name + index.ToString()));

                return name + index.ToString();
            }

            return name;
        }

        void UpdateResources()
        {
#if DEBUG
            TelemetryRadioLogger.Print("Updating resources data.");
#endif
            partResources.Clear();
            totalResources.Clear();
            foreach (Part part in vessel.parts)
            {
                string partName = GetUniquePartName(part);
                if (part.Resources.Count > 0) partResources.Add(partName, new List<ResourceInfo>());

                foreach (PartResource resource in part.Resources.list)
                {
                    ResourceInfo item = new ResourceInfo
                    {
                        stage = part.inverseStage,
                        currentAmount = resource.amount,
                        maxAmount = resource.maxAmount,
                        density = resource.info.density,
                        resourceName = resource.resourceName
                    };

                    partResources[partName].Add(item);

                    ResourceInfo totalItem;
                    if (totalResources.ContainsKey(resource.resourceName))
                    {
                        totalItem = totalResources[resource.resourceName];
                        totalItem.currentAmount += resource.amount;
                        totalItem.maxAmount += resource.maxAmount;
                        totalResources[resource.resourceName] = totalItem;
                    }
                    else
                    {
                        totalItem = new ResourceInfo
                        {
                            stage = 0,
                            currentAmount = resource.amount,
                            maxAmount = resource.maxAmount,
                            density = resource.info.density,
                            resourceName = resource.resourceName
                        };
                        totalResources.Add(resource.resourceName, totalItem);
                    }
                }
            }
        }

        #endregion

        #region Engine Data Functions ("Borrowed" from RasterPropMonitor)

        double GetCurrentThrust(PartModule engine)
        {
            // "Borrowed" from RasterPropMonitor
            var straightEngine = engine as ModuleEngines;
            var flippyEngine = engine as ModuleEnginesFX;
            if (straightEngine != null)
            {
                if ((!straightEngine.EngineIgnited) || (!straightEngine.isEnabled) || (!straightEngine.isOperational))
                    return 0;
                return straightEngine.finalThrust;
            }
            if (flippyEngine != null)
            {
                if ((!flippyEngine.EngineIgnited) || (!flippyEngine.isEnabled) || (!flippyEngine.isOperational))
                    return 0;
                return flippyEngine.finalThrust;
            }
            return 0;
        }

        // "Borrowed" from RasterPropMonitor
        double GetMaximumThrust(PartModule engine)
        {
            var straightEngine = engine as ModuleEngines;
            var flippyEngine = engine as ModuleEnginesFX;
            if (straightEngine != null)
            {
                if ((!straightEngine.EngineIgnited) || (!straightEngine.isEnabled) || (!straightEngine.isOperational))
                    return 0;
                return straightEngine.maxThrust * (straightEngine.thrustPercentage / 100d);
            }
            if (flippyEngine != null)
            {
                if ((!flippyEngine.EngineIgnited) || (!flippyEngine.isEnabled) || (!flippyEngine.isOperational))
                    return 0;
                return flippyEngine.maxThrust * (flippyEngine.thrustPercentage / 100d);
            }
            return 0;
        }

        // "Borrowed" from RasterPropMonitor
        double GetRealIsp(PartModule engine)
        {
            var straightEngine = engine as ModuleEngines;
            var flippyEngine = engine as ModuleEnginesFX;
            if (straightEngine != null)
            {
                if ((!straightEngine.EngineIgnited) || (!straightEngine.isEnabled) || (!straightEngine.isOperational))
                    return 0;
                return straightEngine.realIsp;
            }
            if (flippyEngine != null)
            {
                if ((!flippyEngine.EngineIgnited) || (!flippyEngine.isEnabled) || (!flippyEngine.isOperational))
                    return 0;
                return flippyEngine.realIsp;
            }
            return 0;
        }
        #endregion

        #endregion

        #region Data Collection and Processing

        #region Processed Data

        public double Longitude
        {
            get
            {
                double longitude = vessel.longitude;
                while (longitude < -180) longitude += 360;
                while (longitude >= 180) longitude -= 360;
                return Format(longitude);
            }
        }

        public double TotalCurrentThrust
        {
            get
            {
                return Format(totalCurrentThrust);
            }
        }

        public double TotalMaxThrust
        {
            get
            {
                return Format(totalMaxThrust);
            }
        }

        public double TotalIsp
        {
            get
            {
                return Format(totalIsp);
            }
        }

        #endregion

        #region Vessel Vectors

        public double AccelerationX
        {
            get
            {
                return Format(vessel.acceleration.x);
            }
        }

        public double AccelerationY
        {
            get
            {
                return Format(vessel.acceleration.y);
            }
        }

        public double AccelerationZ
        {
            get
            {
                return Format(vessel.acceleration.z);
            }
        }

        public double Acceleration
        {
            get
            {
                return Format(vessel.acceleration.magnitude);
            }
        }

        public double AngularMomentum
        {
            get
            {
                return Format(vessel.angularMomentum.magnitude);
            }
        }

        public double AngularMomentumX
        {
            get
            {
                return Format(vessel.angularMomentum.x);
            }
        }

        public double AngularMomentumY
        {
            get
            {
                return Format(vessel.angularMomentum.y);
            }
        }

        public double AngularMomentumZ
        {
            get
            {
                return Format(vessel.angularMomentum.z);
            }
        }

        public double AngularVelocityX
        {
            get
            {
                return Format(vessel.angularVelocity.x);
            }
        }

        public double AngularVelocityY
        {
            get
            {
                return Format(vessel.angularVelocity.y);
            }
        }

        public double AngularVelocityZ
        {
            get
            {
                return Format(vessel.angularVelocity.z);
            }
        }

        public double AngularVelocity
        {
            get
            {
                return Format(vessel.angularVelocity.magnitude);
            }
        }

        public double OrbitalVelocityX
        {
            get
            {
                return Format(vessel.obt_velocity.x);
            }
        }

        public double OrbitalVelocityY
        {
            get
            {
                return Format(vessel.obt_velocity.y);
            }
        }

        public double OrbitalVelocityZ
        {
            get
            {
                return Format(vessel.obt_velocity.z);
            }
        }

        public double OrbitalVelocity
        {
            get
            {
                return Format(vessel.obt_velocity.magnitude);
            }
        }

        public double SurfaceVelocityX
        {
            get
            {
                return Format(vessel.srf_velocity.x);
            }
        }

        public double SurfaceVelocityY
        {
            get
            {
                return Format(vessel.srf_velocity.y);
            }
        }

        public double SurfaceVelocityZ
        {
            get
            {
                return Format(vessel.srf_velocity.z);
            }
        }

        public double SurfaceVelocity
        {
            get
            {
                return Format(vessel.srf_velocity.magnitude);
            }
        }
        #endregion

        #region Planetarium Data

        public double UniversalTime
        {
            get
            {
                return Format(Planetarium.GetUniversalTime());
            }
        }

        #endregion

        #region Crew Information

        public int CrewCount
        {
            get
            {
                return vessel.GetCrewCount();
            }
        }

        #endregion

        #region Vessel Raw Data

        public double Altitude { get { return Format(vessel.altitude); } }
        public double AtmosphericDensity { get { return Format(vessel.atmDensity); } }
        public int CurrentStage { get { return vessel.currentStage; } }
        public double GeeForce { get { return Format(vessel.geeForce); } }
        public double HeightFromTerrain { get { return Format(vessel.heightFromTerrain); } }
        public double Latitude { get { return Format(vessel.latitude); } }
        public double MissionTime { get { return Format(vessel.missionTime); } }
        public double OrbitSpeed { get { return Format(vessel.obt_speed); } }
        public double SurfaceSpeed { get { return Format(vessel.srfSpeed); } }
        public double StaticPressure { get { return Format(vessel.staticPressure, 4); } }
        public double VerticalSpeed { get { return Format(vessel.verticalSpeed); } }
        public double TerrainAltitude { get { return Format(vessel.terrainAltitude); } }
        public string VesselName { get { return vessel.vesselName; } }

        #endregion

        #region Trajectory Data

        public double Apoapsis { get { return Format(vessel.GetOrbit().ApA); } }
        public double Periapsis { get { return Format(vessel.GetOrbit().PeA); } }
        public double TimeToApoapsis { get { return Format(vessel.GetOrbit().timeToAp); } }
        public double TimeToPeriapsis { get { return Format(vessel.GetOrbit().timeToPe); } }
        public double LongitudeOfAscendingNode { get { return Format(vessel.GetOrbit().LAN); } }
        public double Inclination { get { return Format(vessel.GetOrbit().inclination); } }
        public double Eccentricity { get { return Format(vessel.GetOrbit().eccentricity); } }
        public double MeanAnomaly { get { return Format(vessel.GetOrbit().meanAnomaly); } }
        public double Period { get { return Format(vessel.GetOrbit().period); } }
        public double TrueAnomaly { get { return Format(vessel.GetOrbit().trueAnomaly); } }

        #endregion

        #region Network Data

        public int ClientCount
        {
            get
            {
                return server.Clients.Length;
            }
        }

        public string[] ClientEndpoints
        {
            get
            {
                List<string> addresses = new List<string>();
                foreach (System.Net.IPEndPoint endpoint in server.Clients)
                {
                    addresses.Add(endpoint.ToString());
                }
                return addresses.ToArray();
            }
        }

        public string ServerEndpoint
        {
            get
            {
                return server.ServerEndpoint.ToString();
            }
        }

        #endregion

        #region Vessel Orientation

        public double Pitch { get { return Format(pitch); } }
        public double Roll { get { return Format(roll); } }
        public double Heading { get { return Format(heading); } }

        #endregion

        #endregion
    }
}
