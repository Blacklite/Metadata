using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadatumResolutionContext<out TMetadata> : IServicesContext
        where TMetadata : IMetadata
    {
        TMetadata Metadata { get; }

        Type MetadatumType { get; }
    }
}
