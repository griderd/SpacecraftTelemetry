using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP;

namespace InertialMeasurementUnit
{
    public class IMUModule : PartModule 
    {
        Vector3d position;
        Vector3d velocity;

        Vector3d orbitalMomenum;
        Vector3d eccentricityVector;
        Vector3d nodeVector;

        double eccentricAnomaly;

        double trueAnomalyRad;
        double inclinationRad;
        double eccentricity;
        double semiMajorAxis;
        double argumentOfPeriapsisRad;
        double longitudeOfAscendingNodeRad; 
        double meanAnomalyRad;
        double apoapsis;
        double periapsis;
        double velocityAtApoapsis;
        double velocityAtPeriapsis;

        public double TrueAnomaly { get { return trueAnomalyRad * (180 / Math.PI); } }
        public double Inclination { get { return inclinationRad * (180 / Math.PI); } }
        public double ArgumentOfPeriapsis { get { return argumentOfPeriapsisRad * (180 / Math.PI); } }
        public double LongitudeOfAscendingNode { get { return longitudeOfAscendingNodeRad * (180 / Math.PI); } }
        public double MeanAnomaly { get { return meanAnomalyRad * (180 / Math.PI); } }
        public double Eccentricity { get { return eccentricity; } }
        public double SemimajorAxis { get { return semiMajorAxis; } }
        public double Apoapsis { get { return apoapsis; } }
        public double Periapsis { get { return periapsis; } }
        public double VelocityAtApoapsis { get { return velocityAtApoapsis; } }
        public double VelocityAtPeriapsis { get { return velocityAtPeriapsis; } }

        [KSPField(isPersistant=true, guiActiveEditor=true, guiActive=false, guiFormat="G", guiName="IMU Sensitivity")]
        [UI_FieldFloatRange(controlEnabled=true, maxValue="10", minValue="0", scene=UI_Scene.Editor)]
        public int accelerometerSensitivity;

        bool run;

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            velocity = new Vector3d(double.Parse(node.GetValue("velocityX")),
                                    double.Parse(node.GetValue("velocityY")),
                                    double.Parse(node.GetValue("velocityZ")));

            position = new Vector3d(double.Parse(node.GetValue("positionX")),
                                    double.Parse(node.GetValue("positionY")),
                                    double.Parse(node.GetValue("positionZ")));
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);

            node.AddValue("velocityX", velocity.x.ToString());
            node.AddValue("velocityY", velocity.y.ToString());
            node.AddValue("velocityZ", velocity.z.ToString());

            node.AddValue("positionX", position.x.ToString());
            node.AddValue("positionY", position.y.ToString());
            node.AddValue("positionZ", position.z.ToString());
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if ((state != StartState.Editor) & (state != StartState.None))
            {
                position = vessel.GetWorldPos3D();
                velocity = vessel.obt_velocity;
                run = true;
            }
            else
            {
                run = false;
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (run)
            {
                Vector3d acceleration = new Vector3d(Math.Round(vessel.acceleration.x, accelerometerSensitivity),
                                                     Math.Round(vessel.acceleration.y, accelerometerSensitivity),
                                                     Math.Round(vessel.acceleration.z, accelerometerSensitivity));
                velocity = velocity + (acceleration * UnityEngine.Time.fixedDeltaTime);
                position = position + (velocity * UnityEngine.Time.fixedDeltaTime);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (run)
            {
                orbitalMomenum = Vector3d.Cross(velocity, position);
                eccentricityVector = (Vector3d.Cross(velocity, orbitalMomenum) / vessel.mainBody.gravParameter) - (position / position.magnitude);
                nodeVector = new Vector3d(-orbitalMomenum.y, orbitalMomenum.x, 0);

                if (Vector3d.Dot(position, velocity) >= 0.0)
                {
                    trueAnomalyRad = Math.Acos(Vector3d.Dot(eccentricityVector, position) / (eccentricityVector.magnitude * position.magnitude));
                }
                else
                {
                    trueAnomalyRad = (2.0 * Math.PI) - Math.Acos(Vector3d.Dot(eccentricityVector, position) / (eccentricityVector.magnitude * position.magnitude));
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
                semiMajorAxis = 1 / ((2 / position.magnitude) - (Math.Pow(velocity.magnitude, 2.0) / vessel.mainBody.gravParameter));

                apoapsis = semiMajorAxis * (1 + eccentricity) - vessel.mainBody.Radius;
                periapsis = semiMajorAxis * (1 - eccentricity) - vessel.mainBody.Radius;
                velocityAtApoapsis = Math.Sqrt(((1 + eccentricity) * vessel.mainBody.gravParameter) / periapsis);
                velocityAtPeriapsis = Math.Sqrt(((1 - eccentricity) * vessel.mainBody.gravParameter) / apoapsis);
            }
        }
    }
}
