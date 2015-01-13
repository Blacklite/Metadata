#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadatumResolutionContext<out TMetadata> : IServicesContext
        where TMetadata : IMetadata
    {
        TMetadata Metadata { get; }

        Type MetadatumType { get; }
    }
}
