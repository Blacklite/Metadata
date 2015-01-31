using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IApplicationMetadataProvider
    {
        IApplicationTypeMetadata GetMetadata<T>();

        IApplicationTypeMetadata GetMetadata(Type type);

        IApplicationTypeMetadata GetMetadata(TypeInfo typeInfo);
    }

    class ApplicationMetadataProvider : IApplicationMetadataProvider
    {
        private readonly IPropertyMetadataProvider _metadataPropertyProvider;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, IApplicationTypeMetadata> _metadata = new ConcurrentDictionary<Type, IApplicationTypeMetadata>();

        public ApplicationMetadataProvider(IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _metadataPropertyProvider = metadataPropertyProvider;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;
        }

        public IApplicationTypeMetadata GetMetadata(TypeInfo typeInfo) => GetUnderlyingMetadata(typeInfo.AsType());

        public IApplicationTypeMetadata GetMetadata(Type type) => GetUnderlyingMetadata(type);

        public IApplicationTypeMetadata GetMetadata<T>() => GetUnderlyingMetadata(typeof(T));

        private IApplicationTypeMetadata GetUnderlyingMetadata(Type type) => _metadata.GetOrAdd(type, x => CreateTypeMetadata(x));

        private IApplicationTypeMetadata CreateTypeMetadata(Type type)
        {
            return new ApplicationTypeMetadata(type, _serviceProvider, _metadataPropertyProvider, _metadatumResolverProvider);
        }
    }
}
