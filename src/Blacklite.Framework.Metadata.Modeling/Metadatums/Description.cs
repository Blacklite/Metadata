using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Description : SimpleMetadatum<string>
    {
        public Description(string value) : base(value) { }
    }
}