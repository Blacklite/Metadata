using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;

namespace Blacklite.Framework.Metadata.Storage.Metadatums.Resolvers
{
    public class MetadataStorageTypeMetadatumResolver : TypeMetadatumResolver<IMetadatum>
    {
        private readonly IMetadataStorageContainer _store;
        public MetadataStorageTypeMetadatumResolver(IMetadataStorageContainer store)
        {
            _store = store;
        }

        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = 1000;

        public override bool CanResolve<T>(ITypeMetadatumResolutionContext context) => _store.Has<T>(context.Metadata);

        public override T Resolve<T>(ITypeMetadatumResolutionContext context) => _store.Get<T>(context.Metadata);
    }

    public class MetadataStoragePropertyMetadatumResolver : PropertyMetadatumResolver<IMetadatum>
    {
        private readonly IMetadataStorageContainer _store;
        public MetadataStoragePropertyMetadatumResolver(IMetadataStorageContainer store)
        {
            _store = store;
        }

        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = 1000;

        public override bool CanResolve<T>(IPropertyMetadatumResolutionContext context) => _store.Has<T>(context.Metadata);

        public override T Resolve<T>(IPropertyMetadatumResolutionContext context) => _store.Get<T>(context.Metadata);
    }
}
