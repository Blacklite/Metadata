using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{

    public interface IMetadatumResolverProviderCollector
    {
        string Key { get; }

        IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> TypeResolvers { get; }

        IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> PropertyResolvers { get; }
    }

    public static class MetadatumResolverProviderCollectorHelper
    {
        public static IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<TMetadata>>> GetMetadatumResolverDictionary<TResolver, TMetadata>(IEnumerable<TResolver> resolvers)
            where TResolver : IMetadatumResolver<TMetadata>
            where TMetadata : IMetadata
        {
            var descriptors = resolvers.Select(x => new MetadatumResolverDescriptor<TResolver, TMetadata>(x));
            var globalDescriptors = descriptors.Where(x => x.IsGlobal);

            return descriptors
                .Where(x => !x.IsGlobal)
                        .GroupBy(x => x.MetadatumType)
                        .ToDictionary(x => x.Key, x =>
                            x.Union(globalDescriptors)
                             .OrderByDescending(z => z.Priority)
                             .Cast<IMetadatumResolverDescriptor<TMetadata>>()
                             .AsEnumerable());
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

        public IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> PropertyResolvers { get; }

        public IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> TypeResolvers { get; }
    }

    class MetadatumResolverProviderCollector : IMetadatumResolverProviderCollector
    {
        public MetadatumResolverProviderCollector(
            IEnumerable<ITypeMetadatumResolver> typeMetadatumResolvers,
            IEnumerable<IPropertyMetadatumResolver> propertyMetadatumResolvers)
        {
            TypeResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<ITypeMetadatumResolver, ITypeMetadata>(typeMetadatumResolvers);
            PropertyResolvers = MetadatumResolverProviderCollectorHelper.GetMetadatumResolverDictionary<IPropertyMetadatumResolver, IPropertyMetadata>(propertyMetadatumResolvers);
        }

        public string Key { get; } = "Default";

        public IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> PropertyResolvers { get; }

        public IDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> TypeResolvers { get; }
    }

}