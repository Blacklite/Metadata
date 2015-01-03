using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.Http;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadata<T> : ITypeMetadata{ }
    class TypeMetadata<TObject> : ITypeMetadata<TObject>, ITypeMetadataInternal
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

        public T Get<T>() where T : class, IMetadatum => _underlyingMetadata.Get<T>();

        public HttpContext HttpContext { get { return (_underlyingMetadata as ITypeMetadataInternal)?.HttpContext; } }

        public void InvalidateMetadatumCache(Type type)
        {
            (_underlyingMetadata as ITypeMetadataInternal)?.InvalidateMetadatumCache(type);
        }

        public override string ToString() => _underlyingMetadata.ToString();
    }
}
