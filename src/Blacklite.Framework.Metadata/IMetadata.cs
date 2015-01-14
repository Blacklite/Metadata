


using System;
using Blacklite.Framework.Metadata.Metadatums;

namespace Blacklite.Framework.Metadata
{



    public interface IMetadata
    {
        string Key { get; }

        T Get<T>() where T : class, IMetadatum;
    }
}
