using System;
using Blacklite.Framework.Metadata.Metadatums;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadata
    {
        string Key { get; }

        T Get<T>() where T : IMetadatum;

        bool InvalidateMetadatumCache(Type type);
    }
}
