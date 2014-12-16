using Blacklite.Framework.Metadata.MetadataProperties;
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

        public PropertyMetadataProvider(IEnumerable<IPropertyDescriptor> propertyDescriptors)
        {
            _propertyDescriptors = propertyDescriptors;
        }

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type =>
                _propertyDescriptors
                    .SelectMany(x => x.Describe(type))
                    .GroupBy(x => x.Name)
                    .Select(x => x.OrderByDescending(z => z.Order).First())
                )
                .Select(x => new PropertyMetadata(parentMetadata, x));

    }
}
