using System;

namespace Blacklite.Framework.Metadata.Metadatums
{
    public class Visible : IMetadatum
    {
        public Visible(bool visible)
        {
            Value = visible;
        }

        public bool Value { get; }
    }
}
