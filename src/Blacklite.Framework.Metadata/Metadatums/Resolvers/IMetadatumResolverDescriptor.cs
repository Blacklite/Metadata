
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolverDescriptor<TMetadata> where TMetadata : IMetadata
    {
        bool IsGlobal { get; }
        Type MetadatumType { get; }
        IMetadatum Resolve(IMetadatumResolutionContext<TMetadata> context);
        bool CanResolve(IMetadatumResolutionContext<TMetadata> context);
    }
}
