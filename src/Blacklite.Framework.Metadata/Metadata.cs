using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadata
    {
        T Get<T>() where T : class, IMetadatum;
    }
}
