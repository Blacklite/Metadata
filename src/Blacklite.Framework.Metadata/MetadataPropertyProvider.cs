using Blacklite.Framework.Metadata.MetadataProperties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadataPropertyProvider
    {
        IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata);
    }

    class MetadataPropertyProvider : IMetadataPropertyProvider
    {
        //private readonly ConcurrentDictionary<Type, IEnumerable<IPropertyMetadata>>
        private readonly IEnumerable<IPropertyDescriptor> _propertyDescriptors;

        public MetadataPropertyProvider(IEnumerable<IPropertyDescriptor> propertyDescriptors)
        {
            _propertyDescriptors = propertyDescriptors;
        }

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata) =>
            _propertyDescriptors
                .SelectMany(x => x.Describe(parentMetadata.Type))
                .GroupBy(x => x.Name)
                .Select(x => new PropertyMetadata(parentMetadata, x.OrderByDescending(z => z.Order).AsEnumerable()));

    }
}
