using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    class MetadataProvider : IMetadataProvider
    {
        private readonly IPropertyMetadataProvider _metadataPropertyProvider;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITypeMetadataActivator _typeMetadataActivator;
        private readonly IApplicationMetadataProvider _metadataProvider;
        private readonly ConcurrentDictionary<Type, ITypeMetadata> _metadata = new ConcurrentDictionary<Type, ITypeMetadata>();

        public MetadataProvider(IApplicationMetadataProvider metadataProvider, ITypeMetadataActivator typeMetadataActivator, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _metadataPropertyProvider = metadataPropertyProvider;
            _typeMetadataActivator = typeMetadataActivator;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;
            _metadataProvider = metadataProvider;
        }

        public ITypeMetadata GetMetadata(TypeInfo typeInfo) => GetUnderlyingMetadata(typeInfo.AsType());

        public ITypeMetadata GetMetadata(Type type) => GetUnderlyingMetadata(type);

        public ITypeMetadata GetMetadata<T>() => GetUnderlyingMetadata(typeof(T));

        private ITypeMetadata GetUnderlyingMetadata(Type type) => _metadata.GetOrAdd(type, CreateTypeMetadata(type));

        private ITypeMetadata CreateTypeMetadata(Type type) => _typeMetadataActivator.Create(_metadataProvider.GetMetadata(type), _serviceProvider, _metadataPropertyProvider, _metadatumResolverProvider);
    }
}
