using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using System;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadataFactory
    {
        ITypeMetadata Create(ITypeMetadata fallback, string key, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider);
    }

    class TypeMetadataFactory : ITypeMetadataFactory
    {
        public ITypeMetadata Create(ITypeMetadata fallback, string key, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            return new TypeMetadata(fallback, key, serviceProvider, metadataPropertyProvider, metadatumResolverProvider);
        }
    }
}
