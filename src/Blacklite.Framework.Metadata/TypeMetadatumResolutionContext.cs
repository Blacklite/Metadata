using Microsoft.Framework.DependencyInjection;
using System;

namespace Blacklite.Framework.Metadata
{
    class TypeMetadatumResolutionContext : IMetadatumResolutionContext<ITypeMetadata>
    {
        public TypeMetadatumResolutionContext(IServiceProvider serviceProvider, ITypeMetadata typeMetadata, Type metadatumType)
        {
            ServiceProvider = serviceProvider;
            Metadata = typeMetadata;
            MetadatumType = metadatumType;
        }

        public Type MetadatumType { get; }

        public ITypeMetadata Metadata { get; }

        public IServiceProvider ServiceProvider { get; }
    }
}
