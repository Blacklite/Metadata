using Blacklite.Framework.Metadata.Modeling.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Mvc.Metadatums
{
    public class HiddenInput : SimpleMetadatum<bool>
    {
        public HiddenInput(bool value) : base(value) { }
    }
}