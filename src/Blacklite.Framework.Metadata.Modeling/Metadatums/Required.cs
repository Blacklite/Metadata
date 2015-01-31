using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Required : SimpleMetadatum<bool>
    {
        public Required(bool value) : base(value) { }
    }
}
