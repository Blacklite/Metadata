using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class InfoType : IMetadatum
    {
        public InfoType(string dataTypeName)
        {
            DataType dataType;
            if (Enum.TryParse(dataTypeName, out dataType))
            {
                DataType = dataType;
            }
            else
            {
                DataType = DataType.Custom;
            }
            Name = dataTypeName;
        }

        public InfoType(DataType dataType)
        {
            DataType = dataType;
            Name = dataType.ToString();
        }

        public string Name { get; }

        public DataType DataType { get; }
    }
}
