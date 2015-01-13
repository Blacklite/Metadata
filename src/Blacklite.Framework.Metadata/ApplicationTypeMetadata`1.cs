using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    class ApplicationTypeMetadata<TObject> : ITypeMetadata<TObject>, IInternalMetadata
    {
        private readonly ITypeMetadata _underlyingMetadata;

        public ApplicationTypeMetadata(IApplicationMetadataProvider metadataProvider)
        {
            // Use a common provider, as it is scoped to the current request
            _underlyingMetadata = metadataProvider.GetMetadata<TObject>();
        }

        public string Key => _underlyingMetadata.Key;

        public string Name => _underlyingMetadata.Name;

        public IEnumerable<IPropertyMetadata> Properties => _underlyingMetadata.Properties;

        public Type Type => _underlyingMetadata.Type;

        public TypeInfo TypeInfo => _underlyingMetadata.TypeInfo;

        public T Get<T>() where T : class, IMetadatum => _underlyingMetadata.Get<T>();

        public void InvalidateMetadatumCache(Type type)
        {
            (_underlyingMetadata as IInternalMetadata)?.InvalidateMetadatumCache(type);
        }

        public override string ToString() => _underlyingMetadata.ToString();
    }
}
