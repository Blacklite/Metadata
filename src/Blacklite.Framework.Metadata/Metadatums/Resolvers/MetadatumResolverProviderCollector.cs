using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{

    public interface IMetadatumResolverProviderCollector
    {
        string Key { get; }

        IMetadatumResolverProviderCollectorItem<ITypeMetadata> TypeResolvers { get; }

        IMetadatumResolverProviderCollectorItem<IPropertyMetadata> PropertyResolvers { get; }
    }

    public static class MetadatumResolverProviderCollectorHelper
    {
        public static IMetadatumResolverProviderCollectorItem<TMetadata> GetMetadatumResolverDictionary<TResolver, TMetadata>(IEnumerable<TResolver> resolvers)
            where TResolver : IMetadatumResolver<TMetadata>
            where TMetadata : IMetadata
        {
            return new MetadatumResolverProviderCollectorItem<TResolver, TMetadata>(resolvers);
        }
    }

    public interface IMetadatumResolverProviderCollectorItem<TMetadata>
            where TMetadata : IMetadata
    {
        IEnumerable<IMetadatumResolverDescriptor<TMetadata>> this[Type type] { get; }
    }

    public class MetadatumResolverProviderCollectorItem<TResolver, TMetadata> : IMetadatumResolverProviderCollectorItem<TMetadata>
            where TResolver : IMetadatumResolver<TMetadata>
            where TMetadata : IMetadata
    {
        private readonly IEnumerable<MetadatumResolverDescriptor<TResolver, TMetadata>> _globalResolverDescriptors;
        private readonly IDictionary<Type, IEnumerable<MetadatumResolverDescriptor<TResolver, TMetadata>>> _specificDescriptors = new Dictionary<Type, IEnumerable<MetadatumResolverDescriptor<TResolver, TMetadata>>>();
        private readonly IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<TMetadata>>> _resolverDictionary = new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<TMetadata>>>();

        public MetadatumResolverProviderCollectorItem(IEnumerable<TResolver> resolvers)
        {
            var descriptors = resolvers.Select(x => new MetadatumResolverDescriptor<TResolver, TMetadata>(x));
            _globalResolverDescriptors = descriptors.Where(x => x.IsGlobal);

            _specificDescriptors = descriptors
                .Where(x => !x.IsGlobal)
                        .GroupBy(x => x.MetadatumType)
                        .ToDictionary(x => x.Key, x => x.AsEnumerable());
        }

        public IEnumerable<IMetadatumResolverDescriptor<TMetadata>> this[Type type]
        {
            get
            {
                if (!typeof(IMetadatum).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                    throw new NotSupportedException("Invalid type");

                IEnumerable<IMetadatumResolverDescriptor<TMetadata>> value;
                if (!_resolverDictionary.TryGetValue(type, out value))
                {
                    IEnumerable<MetadatumResolverDescriptor<TResolver, TMetadata>> specificValues;
                    if (_specificDescriptors.TryGetValue(type, out specificValues))
                    {
                        _specificDescriptors.Remove(type);
                    }
                    else
                    {
                        specificValues = Enumerable.Empty<MetadatumResolverDescriptor<TResolver, TMetadata>>();
                    }

                    value = _globalResolverDescriptors
                        .Union(specificValues)
                        .OrderByDescending(z => z.Priority)
                        .Cast<IMetadatumResolverDescriptor<TMetadata>>()
                        .ToArray();

                    _resolverDictionary.Add(type, value);
                }

                return value;
            }
        }
    }

    class ApplicationMetadatumResolverProviderCollector : IMetadatumResolverProviderCollector
    {
        public ApplicationMetadatumResolverProviderCollector(
            IEnumerable<IApplicationTypeMetadatumResolver> typeMetadatumResolvers,
            IEnumerable<IApplicationPropertyMetadatumResolver> propertyMetadatumResolvers)
        {
            TypeResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<IApplicationTypeMetadatumResolver, ITypeMetadata>(typeMetadatumResolvers);
            PropertyResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<IApplicationPropertyMetadatumResolver, IPropertyMetadata>(propertyMetadatumResolvers);
        }

        public string Key { get; } = "Application";

        public IMetadatumResolverProviderCollectorItem<IPropertyMetadata> PropertyResolvers { get; }

        public IMetadatumResolverProviderCollectorItem<ITypeMetadata> TypeResolvers { get; }
    }

    class ScopedMetadatumResolverProviderCollector : IMetadatumResolverProviderCollector
    {
        public ScopedMetadatumResolverProviderCollector(
            IEnumerable<IScopedTypeMetadatumResolver> typeMetadatumResolvers,
            IEnumerable<IScopedPropertyMetadatumResolver> propertyMetadatumResolvers)
        {
            TypeResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<IScopedTypeMetadatumResolver, ITypeMetadata>(typeMetadatumResolvers);
            PropertyResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<IScopedPropertyMetadatumResolver, IPropertyMetadata>(propertyMetadatumResolvers);
        }

        public string Key { get; } = "Scoped";

        public IMetadatumResolverProviderCollectorItem<IPropertyMetadata> PropertyResolvers { get; }

        public IMetadatumResolverProviderCollectorItem<ITypeMetadata> TypeResolvers { get; }
    }

}