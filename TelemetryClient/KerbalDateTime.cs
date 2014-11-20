using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryClient
{
    public struct KerbalDateTime
    {
        public double TotalSeconds { get; private set; }

        public bool BeforeCommonEpoch { get; private set; }
        public int Year { get; private set; }
        public int Days { get; private set; }
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public KerbalDateTime(double seconds)
            : this()
        {
            BeforeCommonEpoch = (seconds < 0);

            TotalSeconds = seconds;
            int sec = (int)Math.Abs(seconds);
            Year = (sec / 9203545) + 1;
            Days = ((sec - ((Year - 1) * 9203545)) / 21600) + 1;
            Hours = (sec - ((Year - 1) * 9203545) - ((Days - 1) * 21600)) / 3600;
            Minutes = (sec - ((Year - 1) * 9203545) - ((Days - 1) * 21600) - (Hours * 3600)) / 60;
            Seconds = (sec - ((Year - 1) * 9203545) - ((Days - 1) * 21600) - (Hours * 3600) - (Minutes * 60));
        }

        public override string ToString()
        {
            return String.Format("{0:D3}:{1:D3}:{2:D1}:{3:D2}:{4:D2}{5}", Year, Days, Hours, Minutes, Seconds, BeforeCommonEpoch ? " BCE" : "");
        }
    }
}
