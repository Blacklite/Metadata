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
        private readonly IDictionary<string, IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>> _typeResolvers;
        private readonly IDictionary<string, IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>> _propertyResolvers;

        public MetadatumResolverProvider(IEnumerable<IMetadatumResolverProviderCollector> collectors)
        {
            _typeResolvers = collectors.ToDictionary(x => x.Key, x => x.TypeResolvers);
            _propertyResolvers = collectors.ToDictionary(x => x.Key, x => x.PropertyResolvers);
        }

        public IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>> GetTypeResolvers(string key, Type type)
        {
            return _typeResolvers[key][type];
        }

        public IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>> GetPropertyResolvers(string key, Type type)
        {
            return _propertyResolvers[key][type];
        }
    }

}
