using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Length : IMetadatum
    {
        public Length(int min, int? max = null)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }
        public int? Max { get; }
    }
}
