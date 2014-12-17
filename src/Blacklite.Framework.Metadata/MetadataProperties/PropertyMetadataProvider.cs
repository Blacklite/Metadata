using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Blacklite.Framework.Metadata.MetadataProperties
{
    public interface IPropertyMetadataProvider
    {
        IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata);
    }

    class PropertyMetadataProvider : IPropertyMetadataProvider
    {
        private readonly ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>> _describerCache = new ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>>();
        private readonly IEnumerable<IPropertyDescriptor> _propertyDescriptors;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;

        public PropertyMetadataProvider(IEnumerable<IPropertyDescriptor> propertyDescriptors, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _propertyDescriptors = propertyDescriptors;
            _metadatumResolverProvider = metadatumResolverProvider;
        }

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type =>
                _propertyDescriptors
                    .SelectMany(x => x.Describe(type))
                    .GroupBy(x => x.Name)
                    .Select(x => x.OrderByDescending(z => z.Order).First())
                )
                .Select(x => new PropertyMetadata(parentMetadata, x, _metadatumResolverProvider));

    }
}
