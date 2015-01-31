using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class ReadOnly : SimpleMetadatum<bool>
    {
        public ReadOnly(bool value) : base(value) { }
    }
}
