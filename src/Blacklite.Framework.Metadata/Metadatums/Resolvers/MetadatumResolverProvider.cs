using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IMetadatumResolverProvider
    {
        IReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>> GetTypeResolvers();
        IReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>> GetPropertyResolvers();
    }

    class MetadatumResolverProvider : IMetadatumResolverProvider
    {
        public MetadatumResolverProvider()
        {

        }

        public IReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>> GetTypeResolvers()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>> GetPropertyResolvers()
        {
            throw new NotImplementedException();
        }
    }

}
