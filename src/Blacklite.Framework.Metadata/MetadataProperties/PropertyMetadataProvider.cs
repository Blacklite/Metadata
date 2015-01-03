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
        private IEnumerable<IPropertyDescriber> SelectPropertyDescriber(Type type, IEnumerable<IPropertyDescriptor> descriptors) =>
           descriptors.SelectMany(x => x.Describe(type))
                       .GroupBy(x => x.Name)
                       .Select(x => x.OrderByDescending(z => z.Order).First());

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type => SelectPropertyDescriber(type, Descriptors))
                    .Select(x => new PropertyMetadata(parentMetadata, (parentMetadata as ITypeMetadataInternal)?.HttpContext, x, _metadatumResolverProvider));

        protected virtual IEnumerable<IPropertyDescriptor> Descriptors
        {
            get
            {
                return _propertyDescriptors;
            }
        }
    }
}
