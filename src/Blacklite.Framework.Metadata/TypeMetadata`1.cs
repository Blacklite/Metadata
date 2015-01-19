using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    class TypeMetadata<TObject> : ITypeMetadata<TObject>
    {
        private readonly ITypeMetadata _underlyingMetadata;

        public TypeMetadata(IMetadataProvider metadataProvider)
        {
            // Use a common provider, as it is scoped to the current request
            _underlyingMetadata = metadataProvider.GetMetadata<TObject>();
        }

        public string Key => _underlyingMetadata.Key;

        public string Name => _underlyingMetadata.Name;

        public IEnumerable<IPropertyMetadata> Properties => _underlyingMetadata.Properties;

        public Type Type => _underlyingMetadata.Type;

        public TypeInfo TypeInfo => _underlyingMetadata.TypeInfo;

        public T Get<T>() where T : IMetadatum => _underlyingMetadata.Get<T>();

        public bool InvalidateMetadatumCache(Type type) => _underlyingMetadata.InvalidateMetadatumCache(type);

        public override string ToString() => _underlyingMetadata.ToString();
    }
}
