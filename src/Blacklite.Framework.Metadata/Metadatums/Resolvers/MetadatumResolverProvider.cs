using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolverProvider
    {
        IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>> GetTypeResolvers(string key, Type type);
        IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>> GetPropertyResolvers(string key, Type type);
    }

    class MetadatumResolverProvider : IMetadatumResolverProvider
    {
        private readonly IDictionary<string, IMetadatumResolverProviderCollectorItem<ITypeMetadata>> _typeResolvers;
        private readonly IDictionary<string, IMetadatumResolverProviderCollectorItem<IPropertyMetadata>> _propertyResolvers;

        public MetadatumResolverProvider(IEnumerable<IMetadatumResolverProviderCollector> collectors)
        {
            _typeResolvers = collectors.ToDictionary(x => x.Key, x => x.TypeResolvers);
            _propertyResolvers = collectors.ToDictionary(x => x.Key, x => x.PropertyResolvers);
        }

        public IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>> GetTypeResolvers(string key, Type type)
        {
            IMetadatumResolverProviderCollectorItem<ITypeMetadata> resolvers;
            if (!_typeResolvers.TryGetValue(key, out resolvers))
            {
                return Enumerable.Empty<IMetadatumResolverDescriptor<ITypeMetadata>>();
            }

            return resolvers[type];
        }

        public IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>> GetPropertyResolvers(string key, Type type)
        {
            IMetadatumResolverProviderCollectorItem<IPropertyMetadata> resolvers;
            if (!_propertyResolvers.TryGetValue(key, out resolvers))
            {
                return Enumerable.Empty<IMetadatumResolverDescriptor<IPropertyMetadata>>();
            }

            return resolvers[type];
        }
    }

}
