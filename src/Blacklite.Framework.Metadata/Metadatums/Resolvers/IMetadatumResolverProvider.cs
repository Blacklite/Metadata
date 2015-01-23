using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolverProvider
    {
        IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> TypeResolvers { get; }

        IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> PropertyResolvers { get; }

        IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>> ApplicationTypeResolvers { get; }

        IReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>> ApplicationPropertyResolvers { get; }
    }
}
