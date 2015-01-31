using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class ShortName : SimpleMetadatum<string>
    {
        public ShortName(string value) : base(value) { }
    }
}