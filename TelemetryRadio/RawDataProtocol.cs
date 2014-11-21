using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TelemetryRadio
{
    public class RawDataProtocol
    {
        DataProcessor data;
        StringBuilder result;

        public RawDataProtocol(DataProcessor data)
        {
            if (data == null) throw new ArgumentNullException();

            this.data = data;
            result = new StringBuilder();
        }

        void GetData()
        {
            Type dataType = data.GetType();
            PropertyInfo[] properties = dataType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                AddPropertyValue(property);
            }
        }

        void AddPropertyValue(PropertyInfo property)
        {
            if ((property.DeclaringType.Name == "System.String") |
                (property.DeclaringType.Name == "System.Double") |
                (property.DeclaringType.Name == "System.Single") |
                (property.DeclaringType.Name == "System.Int32") |
                (property.DeclaringType.Name == "System.Int64") |
                (property.DeclaringType.Name == "System.Boolean"))
            {
                string value = property.GetValue(data, null).ToString();
                AddItem(property.Name, value);
            }
        }

        void AddItem(string key, string value)
        {
            result.Append(key);
            result.Append(':');
            result.AppendLine(value);
        }

        public override string ToString()
        {
            result.Clear();

            result.AppendLine("begin");
            GetData();
            result.AppendLine("end");

            return result.ToString();
        }
    }
}
