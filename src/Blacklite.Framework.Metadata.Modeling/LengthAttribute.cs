using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : Attribute
    {
        public LengthAttribute(int min, int? max = null)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }
        public int? Max { get; }
    }
}