using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Blacklite.Framework.Metadata.Properties
{
    public interface IPropertyMetadataProvider
    {
        IEnumerable<IPropertyMetadata> GetApplicationProperties(IApplicationTypeMetadata parentMetadata);
        IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata fallbackTypeMetadata, ITypeMetadata parentMetadata, IServiceProvider serviceProvider);
    }

    public class PropertyMetadataProvider : IPropertyMetadataProvider
    {
        private readonly ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>> _describerCache = new ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>>();
        private readonly IEnumerable<IPropertyDescriptor> _propertyDescriptors;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly IServiceProvider _serviceProvider;

        public PropertyMetadataProvider(IServiceProvider serviceProvider, IEnumerable<IPropertyDescriptor> propertyDescriptors, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _serviceProvider = serviceProvider;
            _propertyDescriptors = propertyDescriptors;
            _metadatumResolverProvider = metadatumResolverProvider;
        }

        private IEnumerable<IPropertyDescriber> SelectPropertyDescriber(Type type, IEnumerable<IPropertyDescriptor> descriptors) =>
           descriptors.SelectMany(x => x.Describe(type))
                       .GroupBy(x => x.Name)
                       .Select(x => x.OrderByDescending(z => z.Order).First());

        public IEnumerable<IPropertyMetadata> GetApplicationProperties(IApplicationTypeMetadata parentMetadata) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type => SelectPropertyDescriber(type, Descriptors))
                    .Select(x => new ApplicationPropertyMetadata(parentMetadata, x, _serviceProvider, _metadatumResolverProvider));

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata fallbackTypeMetadata, ITypeMetadata parentMetadata, IServiceProvider serviceProvider) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type => SelectPropertyDescriber(type, Descriptors))
                    .Select(x => new PropertyMetadata(fallbackTypeMetadata.Properties.Single(z => z.Name == x.Name), parentMetadata, serviceProvider, _metadatumResolverProvider));

        protected virtual IEnumerable<IPropertyDescriptor> Descriptors { get { return _propertyDescriptors; } }
    }
}
