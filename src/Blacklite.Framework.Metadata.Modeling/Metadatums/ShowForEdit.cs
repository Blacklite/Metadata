using Blacklite.Framework.Metadata.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class ShowForEdit : SimpleMetadatum<bool>
    {
        public ShowForEdit(bool value) : base(value) { }
    }
}