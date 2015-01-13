using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{
    internal interface IInternalMetadata
    {
        void InvalidateMetadatumCache(Type type);
    }
}
