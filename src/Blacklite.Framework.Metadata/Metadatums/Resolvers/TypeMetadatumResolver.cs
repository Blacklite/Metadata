using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class TypeMetadatumResolver<TMetadatum> : ITypeMetadatumResolver<TMetadatum>, IApplicationTypeMetadatumResolver<TMetadatum>
        where TMetadatum : class, IMetadatum
    {
        public abstract Type GetMetadatumType();

        public abstract int Priority { get; }

        public abstract bool CanResolve<T>(IMetadatumResolutionContext<ITypeMetadata> context) where T : class, IMetadatum;
    }
}
