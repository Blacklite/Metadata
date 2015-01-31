using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ShortNameAttribute : Attribute
    {
        public ShortNameAttribute(string displayName)
        {
            ShortName = displayName;
        }

        public string ShortName { get; }
    }
}