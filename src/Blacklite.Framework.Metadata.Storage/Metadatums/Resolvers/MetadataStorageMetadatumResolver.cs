using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;

namespace Blacklite.Framework.Metadata.Storage.Metadatums.Resolvers
{
    public class MetadataStorageMetadatumResolver : TypeMetadatumResolver<IMetadatum>
    {
        private readonly IMetadataStore _store;
        public MetadataStorageMetadatumResolver(IMetadataStore store)
        {
            _store = store;
        }

        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = 1000;

        public override bool CanResolve<T>(ITypeMetadatumResolutionContext context) => _store.Has<T>(context.Metadata);

        public override T Resolve<T>(ITypeMetadatumResolutionContext context) => _store.Get<T>(context.Metadata);
    }
}
