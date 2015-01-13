using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    class MetadatumResolverProvider : IMetadatumResolverProvider
    {
        private static IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<TMetadata>>> GetMetadatumResolverDictionary<TResolver, TMetadata>(IEnumerable<TResolver> resolvers)
            where TResolver : IMetadatumResolver<TMetadata>
            where TMetadata : IMetadata
        {
            var descriptors = resolvers.Select(x => new MetadatumResolverDescriptor<TResolver, TMetadata>(x));
            var globalDescriptors = descriptors.Where(x => x.IsGlobal);

            var dictionary = descriptors
                .Where(x => !x.IsGlobal)
                        .GroupBy(x => x.MetadatumType)
                        .ToDictionary(x => x.Key, x =>
                            x.Union(globalDescriptors)
                             .OrderByDescending(z => z.Priority)
                             .Cast<IMetadatumResolverDescriptor<TMetadata>>()
                             .AsEnumerable());

            return new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<TMetadata>>>(dictionary);
        }

        public MetadatumResolverProvider(
            IEnumerable<IApplicationTypeMetadatumResolver> applicaitonTypeMetadatumResolvers,
            IEnumerable<IApplicationPropertyMetadatumResolver> applicaitonPropertyMetadatumResolvers,
            IEnumerable<ITypeMetadatumResolver> typeMetadatumResolvers,
            IEnumerable<IPropertyMetadatumResolver> propertyMetadatumResolvers)
        {
            // Null metadatum type isn't invalid, it allows the resolver to
            // operate against all metadatums.
            // For example, a resolver that looks at a persistance store, could possibly
            // resolve every available type of metadatum if it has a value for it.
            ApplicationTypeResolvers = GetMetadatumResolverDictionary<IApplicationTypeMetadatumResolver, ITypeMetadata>(applicaitonTypeMetadatumResolvers);
            TypeResolvers = GetMetadatumResolverDictionary<ITypeMetadatumResolver,ITypeMetadata>(typeMetadatumResolvers);
            ApplicationPropertyResolvers = GetMetadatumResolverDictionary<IApplicationPropertyMetadatumResolver,IPropertyMetadata>(applicaitonPropertyMetadatumResolvers);
            PropertyResolvers = GetMetadatumResolverDictionary<IPropertyMetadatumResolver, IPropertyMetadata>(propertyMetadatumResolvers);
        }

        public IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> TypeResolvers { get; }

        public IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> PropertyResolvers { get; }

        public IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> ApplicationTypeResolvers { get; }

        public IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> ApplicationPropertyResolvers { get; }
    }

}
