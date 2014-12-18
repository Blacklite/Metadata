using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolverProvider
    {
        IReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>> TypeResolvers { get; }
        IReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>> PropertyResolvers { get; }
    }

    class MetadatumResolverProvider : IMetadatumResolverProvider
    {
        public MetadatumResolverProvider(
            IEnumerable<ITypeMetadatumResolver> typeMetadatumResolvers,
            IEnumerable<IPropertyMetadatumResolver> propertyMetadatumResolvers)
        {
            // Null metadatum type isn't invalid, it allows the resolver to
            // operate against all metadatums.
            // For example, a resolver that looks at a persistance store, could possibly
            // resolve every available type of metadatum if it has a value for it.
            var globalTypeMetadatumTypeResolvers = typeMetadatumResolvers
                .Where(x => x.GetMetadatumType() == null);

            TypeResolvers = new ReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>>(
                    typeMetadatumResolvers
                        .Except(globalTypeMetadatumTypeResolvers)
                        .GroupBy(x => x.GetMetadatumType())
                        .ToDictionary(x => x.Key, x =>
                            x.Union(globalTypeMetadatumTypeResolvers)
                             .OrderByDescending(z => z.Priority)
                             .AsEnumerable())
                );

            var globalPropertyMetadatumTypeResolvers = propertyMetadatumResolvers
                .Where(x => x.GetMetadatumType() == null);

            PropertyResolvers = new ReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>>(
                    propertyMetadatumResolvers
                        .Except(globalPropertyMetadatumTypeResolvers)
                        .GroupBy(x => x.GetMetadatumType())
                        .ToDictionary(x => x.Key, x =>
                            x.Union(globalPropertyMetadatumTypeResolvers)
                             .OrderByDescending(z => z.Priority)
                             .AsEnumerable())
                );
        }

        public IReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>> TypeResolvers { get; }

        public IReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>> PropertyResolvers { get; }
    }

}
