using Blacklite.Framework.Metadata.Properties;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class PropertyMetadatumResolver<TMetadatum> : IPropertyMetadatumResolver<TMetadatum>, IApplicationPropertyMetadatumResolver<TMetadatum>
        where TMetadatum : IMetadatum
    {
        public abstract Type GetMetadatumType();

        public abstract int Priority { get; }

        public abstract bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context);
    }
}
