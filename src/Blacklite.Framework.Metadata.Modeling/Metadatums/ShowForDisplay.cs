using Blacklite.Framework.Metadata.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class ShowForDisplay : SimpleMetadatum<bool>
    {
        public ShowForDisplay(bool value) : base(value) { }
    }
}