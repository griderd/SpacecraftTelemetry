using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TelemetryRadio
{
    class RemoteTechCompatibility
    {
        public static bool HasConnection(Vessel vessel)
        {
            PartModule[] antennas = FindRTAntennas(vessel);
#if DEBUG
            TelemetryRadioLogger.Print("RT Antenna Count: " + antennas.Length.ToString());
#endif

            bool state = true;
            foreach (PartModule antenna in antennas)
            {
                state &= AntennaHasConnection(antenna);
            }

#if DEBUG
            TelemetryRadioLogger.Print("Connection state is: " + state.ToString());
#endif
            return state;
        }

        private static bool AntennaHasConnection(PartModule antenna)
        {
            if (antenna == null)
            {
                TelemetryRadioLogger.Print("No RemoteTech antenna installed.");
                return true;
            }
            else
            {
                try
                {
                    RemoteTech.ModuleRTAntenna part = (RemoteTech.ModuleRTAntenna)antenna;
                    return RemoteTech.API.HasAnyConnection(part.Guid);

                    //Type antennaType = antenna.GetType();
                    //PropertyInfo guidProperty = antennaType.GetProperty("Guid");
                    //if (Object.ReferenceEquals(guidProperty, null))
                    //{
                    //    TelemetryRadioLogger.Print("Could not get RemoteTech.ModuleRTAntenna.Guid Property. GetProperty() returned null.");
                    //    return true;
                    //}
                    //Guid antennaGuid = (Guid)guidProperty.GetValue(antenna, null);
                    //TelemetryRadioLogger.Print("ModuleRTAntenna GUID: " + antennaGuid.ToString());
                    //Type api = antennaType.Assembly.GetType("API");
                    //if (Object.ReferenceEquals(api, null))
                    //{
                    //    TelemetryRadioLogger.Print("Could not get RemoteTech.API Class.");
                    //    return true;
                    //}
                    //MethodInfo hasAnyConnectionMethod = api.GetMethod("HasAnyConnection");
                    //if (Object.ReferenceEquals(hasAnyConnectionMethod, null))
                    //{
                    //    TelemetryRadioLogger.Print("Could not get RemoteTech.API.HasAnyConnection(Guid) Method");
                    //    return true;
                    //}
                    //return (bool)hasAnyConnectionMethod.Invoke(antenna, new object[] { antennaGuid });
                }
                catch (Exception ex)
                {
                    TelemetryRadioLogger.Print(ex.Message + " " + ex.StackTrace);
                }

                return true;
            }
        }

        private static PartModule[] FindRTAntennas(Vessel vessel)
        {
            TelemetryRadioLogger.Print("Looking for any RT antenna.");
            try
            {
                List<PartModule> antenna = new List<PartModule>();

                foreach (Part part in vessel.parts)
                {
                    foreach (PartModule module in part.Modules)
                    {
                        if (module.moduleName == "ModuleRTAntenna")
                        {
                            TelemetryRadioLogger.Print("RT antenna found.");
                            antenna.Add(module);
                        }
                    }
                }

                return antenna.ToArray();
            }
            catch (Exception ex)
            {
                TelemetryRadioLogger.Print(ex.Message + " " + ex.StackTrace);
            }

            return null;
        }
    }
}
