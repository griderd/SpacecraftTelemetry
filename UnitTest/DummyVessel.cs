using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest
{
    class DummyVessel
    {
        public double altitude;
        public double atmDensity;
        public int currentStage;
        public float distanceLandedPackThreshold;
        public float distanceLandedUnpackThreshold;
        public float distancePackThreshold;
        public float distanceUnpackThreshold;
        public double geeForce;
        public double geeForce_immediate;
        public float heightFromSurface;
        public float heightFromTerrain;
        public double horizontalSrfSpeed;
        public Guid id;
        public bool Landed;
        public string landedAt;
        public double latitude;
        public double launchTime;
        public static float loadDistance;
        public bool loaded;
        public double longitude;
        public double missionTime;
        public double obt_speed;
        public bool packed;
        public double pqsAltitude;
        public uint referenceTransformId;
        public double specificAcceleration;
        public bool Splashed;
        public double srfSpeed;
        public double staticPressure;
        public double terrainAltitude;
        public static float unloadDistance;
        public double verticalSpeed;
        public string vesselName;
    }
}
