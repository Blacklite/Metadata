﻿using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadataProvider
    {
        ITypeMetadata GetMetadata<T>();

        ITypeMetadata GetMetadata(Type type);

        ITypeMetadata GetMetadata(TypeInfo typeInfo);
    }

    class MetadataProvider : IMetadataProvider
    {
        private readonly IPropertyMetadataProvider _metadataPropertyProvider;
        private readonly ConcurrentDictionary<Type, ITypeMetadata> _metadata = new ConcurrentDictionary<Type, ITypeMetadata>();

        public MetadataProvider(IPropertyMetadataProvider metadataPropertyProvider)
        {
            _metadataPropertyProvider = metadataPropertyProvider;
        }

        public ITypeMetadata GetMetadata(TypeInfo typeInfo) => GetMetadata(typeInfo.AsType());

        public ITypeMetadata GetMetadata(Type type) => GetMetadata(type);

        public ITypeMetadata GetMetadata<T>() => GetMetadata(typeof(T));

        private ITypeMetadata GetUnderlyingMetadata(Type type) => _metadata.GetOrAdd(type, new TypeMetadata(type, _metadataPropertyProvider));
    }


}
