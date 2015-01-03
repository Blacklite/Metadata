using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadata
    {
        string Key { get; }

        T Get<T>() where T : class, IMetadatum;
    }

    internal interface IInternalMetadata
    {
        void InvalidateMetadatumCache(Type type);
    }
}
