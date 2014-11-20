using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryClient
{
    public struct KerbalTimespan
    {
        public bool Positive { get; private set; }
        public int Days { get; private set; }
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public KerbalTimespan(double seconds)
            : this()
        {
            Positive = seconds >= 0;

            int sec = (int)Math.Abs(seconds);
            Days = sec / 21600;
            Hours = (sec - (Days * 21600)) / 3600;
            Minutes = (sec - (Days * 21600) - (Hours * 3600)) / 60;
            Seconds = (sec - (Days * 21600) - (Hours * 3600) - (Minutes * 60));
        }

        public override string ToString()
        {
            return String.Format("{0}{1}:{2}:{3:D2}:{4:D2}", Positive ? "+" : "-", Days, Hours, Minutes, Seconds);
        }
    }
}
