using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata.Metadatums
{
    public interface IMetadatumResolverProvider
    {
        IReadOnlyDictionary<Type, ITypeMetadatumResolver> GetTypeResolvers();
        IReadOnlyDictionary<Type, IPropertyMetadatumResolver> GetPropertyResolvers();
    }

    class MetadatumResolverProvider : IMetadatumResolverProvider
    {
        public MetadatumResolverProvider()
        {

        }

        public IReadOnlyDictionary<Type, ITypeMetadatumResolver> GetTypeResolvers()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<Type, IPropertyMetadatumResolver> GetPropertyResolvers()
        {
            throw new NotImplementedException();
        }
    }

}
