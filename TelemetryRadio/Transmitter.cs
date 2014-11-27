using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelemetryServer;

namespace TelemetryRadio
{
    public class Transmitter
    {
        TelemetryRadioAntenna antenna;

        Queue<TelemetryData> transmissionQueue;

        public Transmitter(TelemetryRadioAntenna antenna)
        {
            transmissionQueue = new Queue<TelemetryData>();
            this.antenna = antenna;
        }

        public void FlushQueue()
        {
            while (transmissionQueue.Count > 0)
            {
                SendNext();
            }
        }

        public void Enqueue(TelemetryData item)
        {
            transmissionQueue.Enqueue(item);
        }

        public void SendNext()
        {
            if ((transmissionQueue.Count > 0) && (RemoteTechCompatibility.HasConnection(antenna.vessel)))
            {
                int length = 0;
                try
                {
                    length = antenna.telemetryServer.Send(transmissionQueue.Dequeue());
                }
                catch (Exception ex)
                {
                    TelemetryRadioLogger.Print(ex.Message + " " + ex.StackTrace);
                }
                TelemetryRadioLogger.Print(length.ToString() + " bytes sent.");
            }
        }
    }
}
