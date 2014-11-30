using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelemetryServer
{
    /// <summary>
    /// Represents the format of the Data property of the DataBlock class.
    /// </summary>
    public enum Protocols : byte
    {
        /// <summary>
        /// Represents raw binary data in the form of a byte array.
        /// </summary>
        Raw,
        /// <summary>
        /// Represents UTF8 text.
        /// </summary>
        String,
        /// <summary>
        /// Represents the TelemetryData structure.
        /// </summary>
        TelemetryData,
        /// <summary>
        /// Represents the CommandUplink structure.
        /// </summary>
        /// <remarks>This protocol is not yet supported.</remarks>
        CommandUplink
    }

    /// <summary>
    /// Represents a block of data for transmission over TCP.
    /// </summary>
    public class DataBlock
    {
        /// <summary>
        /// Represents "begin" in binary.
        /// </summary>
        public readonly byte[] MagicNumber = new byte[] { 0x62, 0x65, 0x67, 0x69, 0x6E };

        /// <summary>
        /// Represents reserved bytes that may be used later.
        /// </summary>
        public readonly byte[] Reserved = new byte[32];

        /// <summary>
        /// Gets the format of the data transmitted.
        /// </summary>
        public Protocols Protocol { get; private set; }

        /// <summary>
        /// Gets the raw binary data.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Creates a new instance using the given format and raw data.
        /// </summary>
        /// <param name="protocol">Format of the binary data.</param>
        /// <param name="binaryData">Raw binary data.</param>
        public DataBlock(Protocols protocol, byte[] binaryData)
        {
            Protocol = protocol;
            Data = binaryData;
        }

        /// <summary>
        /// Translates the entire DataBlock into raw binary, ready for transmission.
        /// </summary>
        /// <returns>Returns a byte array containing the serialized DataBlock.</returns>
        public byte[] ToBytes()
        {
            List<byte> data = new List<byte>();

            data.AddRange(MagicNumber);
            data.Add((byte)Protocol);
            data.AddRange(Reserved);
            data.AddRange(BitConverter.GetBytes(Data.Length));
            data.AddRange(Data);

            return data.ToArray();
        }

        /// <summary>
        /// Translates the data content of the DataBlock into a useable format based on the Protocol property.
        /// </summary>
        /// <returns>Returns an object containing the deserialized data.</returns>
        /// <exception cref="NotSupportedException">Thrown if no known protocol is used, or the protocol is not yet supported.</exception>
        /// <remarks>If you're using a custom protocol, you will want to deserialize the data yourself using the Data property.</remarks>
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
