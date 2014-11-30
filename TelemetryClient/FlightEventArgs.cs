using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryClient
{
    class FlightEventArgs : EventArgs
    {
        public string Sender { get; private set; }
        public string EventString { get; private set; }

        public FlightEventArgs(string sender, string eventString)
        {
            Sender = sender;
            EventString = eventString;
        }
    }
}
