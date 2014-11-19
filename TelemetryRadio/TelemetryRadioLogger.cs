using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TelemetryRadio
{
    class TelemetryRadioLogger
    {
        public static void Print(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[TelemetryRadio] ");
            sb.Append(message);
#if KSP_DEBUG
            MonoBehaviour.print(sb.ToString());
#else
            Console.WriteLine(sb.ToString());
#endif
        }
    }
}
