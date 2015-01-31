using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class DisplayName : SimpleMetadatum<string>
    {
        public DisplayName(string value) : base(value) { }
    }
}