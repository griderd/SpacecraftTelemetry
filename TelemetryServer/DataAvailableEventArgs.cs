using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryServer
{
    public class DataAvailableEventArgs : EventArgs 
    {
        public DataBlock Data { get; private set; }

        public DataAvailableEventArgs(DataBlock data)
        {
            Data = data;
        }
    }
}
