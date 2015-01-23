using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class TypeMetadatumResolver<TMetadatum> : ITypeMetadatumResolver<TMetadatum>, IApplicationTypeMetadatumResolver<TMetadatum>
        where TMetadatum : IMetadatum
    {
        public abstract Type GetMetadatumType();

        public abstract int Priority { get; }

        public abstract bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context);
    }
}
