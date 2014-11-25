using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP;

namespace InertialMeasurementUnit
{
    public class IMUModule : PartModule 
    {
        Vector3d positionVector;
        Vector3d velocityVector;
        Vector3d accelerationVector;

        Vector3d orbitalMomenum;
        Vector3d eccentricityVector;
        Vector3d nodeVector;

        double eccentricAnomaly;
        double trueAnomalyRad;
        double inclinationRad;
        double argumentOfPeriapsisRad;
        double meanAnomalyRad;
        double longitudeOfAscendingNodeRad;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m/s", guiName = "Velocity")]
        public double velocity;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m/s^2", guiName = "Acceleration")]
        public double acceleration;

        [KSPField(guiActive=true, guiFormat="F3", guiUnits="deg", guiName="True Anomaly")]
        public double trueAnomaly;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "deg", guiName = "Inclination")]
        public double inclination;

        [KSPField(guiActive = true, guiFormat = "F3", guiName = "Eccentricity")]
        public double eccentricity;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m", guiName = "Semimajor Axis")]
        public double semiMajorAxis;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "deg", guiName = "True Anomaly")]
        public double argumentOfPeriapsis;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "deg", guiName = "Long of Asc Node")]
        public double longitudeOfAscendingNode;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "deg", guiName = "Mean Anomaly")]
        public double meanAnomaly;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m", guiName = "Apoapsis")]
        public double apoapsis;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m", guiName = "Periapsis")]
        public double periapsis;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m/s", guiName = "Velocity at Apoapsis")]
        public double velocityAtApoapsis;

        [KSPField(guiActive = true, guiFormat = "F3", guiUnits = "m/s", guiName = "Velocity at Periapsis")]
        public double velocityAtPeriapsis;

        public double TrueAnomaly { get { return trueAnomaly; } }
        public double Inclination { get { return inclination; } }
        public double ArgumentOfPeriapsis { get { return argumentOfPeriapsis; } }
        public double LongitudeOfAscendingNode { get { return longitudeOfAscendingNode; } }
        public double MeanAnomaly { get { return meanAnomalyRad; } }
        public double Eccentricity { get { return eccentricity; } }
        public double SemimajorAxis { get { return semiMajorAxis; } }
        public double Apoapsis { get { return apoapsis; } }
        public double Periapsis { get { return periapsis; } }
        public double VelocityAtApoapsis { get { return velocityAtApoapsis; } }
        public double VelocityAtPeriapsis { get { return velocityAtPeriapsis; } }

        [KSPField(guiActiveEditor = true, guiActive = true, guiFormat = "G", guiName = "IMU Sensitivity")]
        [UI_FieldFloatRange(controlEnabled=true, maxValue="10", minValue="0", scene=UI_Scene.Editor)]
        public int accelerometerSensitivity;

        [KSPField(guiActive = true, guiFormat = "g", guiName="Active")]
        public bool run;

        bool started;

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            if (node != null)
            {
                accelerometerSensitivity = node.GetValue("sensitivity") != null ? int.Parse(node.GetValue("sensitivity")) : 2;
                velocityVector = new Vector3d(node.GetValue("velocityX") != null ? double.Parse(node.GetValue("velocityX")) : 0,
                                              node.GetValue("velocityY") != null ? double.Parse(node.GetValue("velocityY")) : 0,
                                              node.GetValue("velocityZ") != null ? double.Parse(node.GetValue("velocityZ")) : 0);

                positionVector = new Vector3d(node.GetValue("positionX") != null ? double.Parse(node.GetValue("positionX")) : 0,
                                              node.GetValue("positionY") != null ? double.Parse(node.GetValue("positionY")) : 0,
                                              node.GetValue("positionZ") != null ? double.Parse(node.GetValue("positionZ")) : 0);
            }
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);

            node.AddValue("sensitivity", accelerometerSensitivity.ToString());

            node.AddValue("velocityX", velocityVector.x.ToString());
            node.AddValue("velocityY", velocityVector.y.ToString());
            node.AddValue("velocityZ", velocityVector.z.ToString());

            node.AddValue("positionX", positionVector.x.ToString());
            node.AddValue("positionY", positionVector.y.ToString());
            node.AddValue("positionZ", positionVector.z.ToString());
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            started = false;

            if ((state != StartState.Editor) & (state != StartState.None))
            {
                run = true;
            }
            else
            {
                run = false;
            }
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (vessel.obt_velocity.magnitude == 0)
            {
                started = false;
                return;
            }
            else
            {
                started = true;
                positionVector = vessel.GetWorldPos3D();
                velocityVector = vessel.obt_velocity;
            }

            if (run)
            {
                accelerationVector = new Vector3d(Math.Round(vessel.acceleration.x, accelerometerSensitivity),
                                                     Math.Round(vessel.acceleration.y, accelerometerSensitivity),
                                                     Math.Round(vessel.acceleration.z, accelerometerSensitivity));
                velocityVector = velocityVector + (accelerationVector * UnityEngine.Time.deltaTime);
                positionVector = positionVector + (velocityVector * UnityEngine.Time.deltaTime);

                orbitalMomenum = Vector3d.Cross(velocityVector, positionVector);
                eccentricityVector = (Vector3d.Cross(velocityVector, orbitalMomenum) / vessel.mainBody.gravParameter) - (positionVector / positionVector.magnitude);
                nodeVector = new Vector3d(-orbitalMomenum.y, orbitalMomenum.x, 0);

                if (Vector3d.Dot(positionVector, velocityVector) >= 0.0)
                {
                    trueAnomalyRad = Math.Acos(Vector3d.Dot(eccentricityVector, positionVector) / (eccentricityVector.magnitude * positionVector.magnitude));
                }
                else
                {
                    trueAnomalyRad = (2.0 * Math.PI) - Math.Acos(Vector3d.Dot(eccentricityVector, positionVector) / (eccentricityVector.magnitude * positionVector.magnitude));
                }

                inclinationRad = Math.Acos(orbitalMomenum.z / orbitalMomenum.magnitude);
                eccentricity = eccentricityVector.magnitude;
                eccentricAnomaly = 2.0 * Math.Atan2(Math.Tan(trueAnomalyRad / 2), Math.Sqrt((1 + eccentricity) / (1 - eccentricity)));

                if (nodeVector.y >= 0)
                {
                    longitudeOfAscendingNodeRad = Math.Acos(nodeVector.x / nodeVector.magnitude);
                }
                else
                {
                    longitudeOfAscendingNodeRad = (2.0 * Math.PI) - Math.Acos(nodeVector.x / nodeVector.magnitude);
                }

                if (eccentricityVector.z >= 0)
                {
                    argumentOfPeriapsisRad = Math.Acos(Vector3d.Dot(nodeVector, eccentricityVector) / (nodeVector.magnitude * eccentricityVector.magnitude));
                }
                else
                {
                    argumentOfPeriapsisRad = (2.0 * Math.PI) - Math.Acos(Vector3d.Dot(nodeVector, eccentricityVector) / (nodeVector.magnitude * eccentricityVector.magnitude));
                }

                meanAnomalyRad = eccentricAnomaly - (eccentricity * Math.Sin(eccentricAnomaly));
                semiMajorAxis = 1 / ((2 / positionVector.magnitude) - (Math.Pow(velocityVector.magnitude, 2.0) / vessel.mainBody.gravParameter));

                apoapsis = semiMajorAxis * (1 + eccentricity);
                periapsis = semiMajorAxis * (1 - eccentricity);
                velocityAtApoapsis = Math.Sqrt(((1 + eccentricity) * vessel.mainBody.gravParameter) / apoapsis);
                velocityAtPeriapsis = Math.Sqrt(((1 - eccentricity) * vessel.mainBody.gravParameter) / periapsis);

                argumentOfPeriapsis = argumentOfPeriapsisRad * (180 / Math.PI);
                longitudeOfAscendingNode = longitudeOfAscendingNodeRad * (180 / Math.PI);
                trueAnomaly = trueAnomalyRad * (180 / Math.PI);
                inclination = inclinationRad * (180 / Math.PI);
                meanAnomaly = meanAnomalyRad * (180 / Math.PI);

                velocity = velocityVector.magnitude;
                acceleration = vessel.acceleration.magnitude;
            }
        }
    }
}
