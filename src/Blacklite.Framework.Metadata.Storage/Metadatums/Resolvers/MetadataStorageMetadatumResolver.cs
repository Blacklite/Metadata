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

        public override bool CanResolve<T>(IMetadatumResolutionContext<ITypeMetadata> context) => _store.Has<T>(context.Metadata);

        public T Resolve<T>(IMetadatumResolutionContext<ITypeMetadata> context)
            where T : class, IMetadatum => _store.Get<T>(context.Metadata);
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

        public override bool CanResolve<T>(IMetadatumResolutionContext<IPropertyMetadata> context) => _store.Has<T>(context.Metadata);

        public T Resolve<T>(IMetadatumResolutionContext<IPropertyMetadata> context)
            where T : class, IMetadatum => _store.Get<T>(context.Metadata);
    }
}
