using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KSP;

namespace RadioTester
{
    class Program
    {
        static TelemetryRadio.TelemetryRadioAntenna antenna;

        static void Main(string[] args)
        {
            antenna = new TelemetryRadio.TelemetryRadioAntenna();
            antenna.OnStart(PartModule.StartState.Flying);

            while (true)
            {
                antenna.OnUpdate();
                Thread.Sleep(100);
            }

        }

    }
}
