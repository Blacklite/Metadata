﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadata<T> : ITypeMetadata { }
    class TypeMetadata<TObject> : ITypeMetadata<TObject>
    {
        private readonly ITypeMetadata _underlyingMetadata;

        public TypeMetadata(IMetadataProvider metadataProvider, IPropertyMetadataProvider metadataPropertyProvider)
        {
            // Use a common provider, as it is "scoped" to the current request
            _underlyingMetadata = metadataProvider.GetMetadata<TObject>();
        }

        public string Name => _underlyingMetadata.Name;

        public IEnumerable<IPropertyMetadata> Properties => _underlyingMetadata.Properties;

        public Type Type => _underlyingMetadata.Type;

        public TypeInfo TypeInfo => _underlyingMetadata.TypeInfo;

        public T Get<T>() where T : IMetadatum => _underlyingMetadata.Get<T>();
    }
}
