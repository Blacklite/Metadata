using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolver
    {
        Type GetMetadatumType();
        int Priority { get; }
    }

    public interface IMetadatumResolver<TMetadata> : IMetadatumResolver
        where TMetadata : IMetadata
    {
        bool CanResolve(IMetadatumResolutionContext<TMetadata> context);
    }

    public interface IMetadatumResolver<TMetadata, TMetadatum> : IMetadatumResolver<TMetadata>
        where TMetadata : IMetadata
        where TMetadatum : IMetadatum
    {
    }
}
