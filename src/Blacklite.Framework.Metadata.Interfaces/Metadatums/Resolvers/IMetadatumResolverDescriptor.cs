#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadatumResolverDescriptor<TMetadata> where TMetadata : IMetadata
    {
        bool IsGlobal { get; }
        Type MetadatumType { get; }
        T Resolve<T>(IMetadatumResolutionContext<TMetadata> context) where T : class, IMetadatum;
        bool CanResolve<T>(IMetadatumResolutionContext<TMetadata> context) where T : class, IMetadatum;
    }
}
