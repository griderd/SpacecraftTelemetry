using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryServer
{
    public enum Protocols : byte
    {
        Raw,
        String,
        TelemetryData,
        CommandUplink
    }

    public class DataBlock
    {
        /// <summary>
        /// Represents "begin" in binary.
        /// </summary>
        public byte[] MagicNumber = new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E };

        public Protocols Protocol { get; private set; }

        public byte[] Data { get; private set; }

        public DataBlock(Protocols protocol, byte[] binaryData)
        {
            Protocol = protocol;
            Data = binaryData;
        }

        public byte[] ToBytes()
        {
            List<byte> data = new List<byte>();

            data.AddRange(MagicNumber);
            data.Add((byte)Protocol);
            data.AddRange(BitConverter.GetBytes(Data.Length));
            data.AddRange(Data);

            return data.ToArray();
        }

        public object GetData()
        {
            switch (Protocol)
            {
                case Protocols.Raw:
                    return Data;

                case Protocols.String:
                    return Encoding.UTF8.GetString(Data);

                case Protocols.TelemetryData:
                    return TelemetryData.Deserialize(Data);

                case Protocols.CommandUplink:
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
