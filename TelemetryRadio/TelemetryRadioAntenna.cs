using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelemetryServer;
using KSP;
using UnityEngine;

namespace TelemetryRadio
{
    public class TelemetryRadioAntenna : PartModule
    {
        Server telemetryServer;
        double lastTime;
        bool collectingData = false;

        DataProcessor processor;

        [KSPField]
        public string ipAddress;

        [KSPField]
        public int port;

        void PrintLog(string message)
        {
            print("[TelemetryRadio] " + message);
        }

        public override void OnStart(PartModule.StartState state)
        {
            if (state == StartState.Editor | state == StartState.None)
            {
                if (telemetryServer != null)
                {
                    TelemetryRadioLogger.Print("In editor or invalid state. Stopping the server.");
                    telemetryServer.StopServer();
                }
                return;
            }

            LinkupToFlightEvents();

            lastTime = Time.timeSinceLevelLoad;

            TelemetryRadioLogger.Print("Initializing server at " + ipAddress + ":" + port.ToString());
            try
            {
                telemetryServer = new Server(ipAddress, port);
            }
            catch (Exception ex)
            {
                TelemetryRadioLogger.Print("Exception: " + ex.Message + " " + ex.StackTrace);
                return;
            }
            TelemetryRadioLogger.Print("Server initialized. Finishing setup.");

            telemetryServer.ClientConnected += new EventHandler<ClientConnectEventArgs>(telemetryServer_ClientConnected);
            processor = new DataProcessor(vessel, telemetryServer);
        }

        void telemetryServer_ClientConnected(object sender, ClientConnectEventArgs e)
        {
            // TODO: Add a status update here
            TelemetryRadioLogger.Print("Client connected from " + e.ClientSocket.RemoteEndPoint.ToString());
            //throw new NotImplementedException();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            bool hasConnection = RemoteTechCompatibility.HasConnection(vessel);
            if (collectingData)
                TelemetryRadioLogger.Print("Still collecting data...");
            else if (!(lastTime + 0.25 <= Time.timeSinceLevelLoad))
                TelemetryRadioLogger.Print("Not enough time elapsed.");
            else
                TelemetryRadioLogger.Print("Trying to start data collection.");

            if ((lastTime + 0.25 <= Time.timeSinceLevelLoad) && (!collectingData) && (hasConnection))
            {
                collectingData = true;
#if DEBUG
                TelemetryRadioLogger.Print("Updating data collectors.");
#endif
                // Update data processor with latest data
                processor.Update();

                TelemetryRadioLogger.Print("Sending data...");
                int length = 0;
                try
                {
                    length = telemetryServer.Send(processor.GetTelemetryData());
                }
                catch (Exception ex)
                {
                    TelemetryRadioLogger.Print(ex.Message + " " + ex.StackTrace);
                }
                TelemetryRadioLogger.Print(length.ToString() + " bytes sent.");

                lastTime = Time.timeSinceLevelLoad;
                collectingData = false;
            }
        }

        void onPartDestroyed(Part part)
        {
            if (part == this.part)
            {
                // Stop the server
                if (telemetryServer != null)
                {
                    TelemetryRadioLogger.Print("Stopping the server.");
                    telemetryServer.StopServer();
                }
            }
        }

        void onPartJointBreak(PartJoint joint)
        {
            if (joint.Child == this.part)
            {
                onPartDestroyed(part);
            }
        }

        void LinkupToFlightEvents()
        {
            GameEvents.onPartDestroyed.Add(new EventData<Part>.OnEvent(onPartDestroyed));
            GameEvents.onPartDie.Add(new EventData<Part>.OnEvent(onPartDestroyed));
            GameEvents.onPartJointBreak.Add(new EventData<PartJoint>.OnEvent(onPartJointBreak));
        }
    }
}
