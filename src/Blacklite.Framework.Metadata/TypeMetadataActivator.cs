using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using System;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadataActivator
    {
        ITypeMetadata Create(ITypeMetadata fallback, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider);
    }

    class TypeMetadataActivator : ITypeMetadataActivator
    {
        public ITypeMetadata Create(ITypeMetadata fallback, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            return new TypeMetadata(fallback, serviceProvider, metadataPropertyProvider, metadatumResolverProvider);
        }
    }
}
