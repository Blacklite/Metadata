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

        public override bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context) => _store.Has(context.Metadata, context.MetadatumType);

        public IMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context) => _store.Get(context.Metadata, context.MetadatumType);
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

        public override bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context) => _store.Has(context.Metadata, context.MetadatumType);

        public IMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context) => _store.Get(context.Metadata, context.MetadatumType);
    }
}
