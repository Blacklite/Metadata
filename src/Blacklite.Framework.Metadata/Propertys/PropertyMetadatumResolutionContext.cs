using System;

namespace Blacklite.Framework.Metadata.Properties
{
    class PropertyMetadatumResolutionContext : IMetadatumResolutionContext<IPropertyMetadata>
    {
        public PropertyMetadatumResolutionContext(IServiceProvider serviceProvider, IPropertyMetadata propertyMetadata, Type metadatumType)
        {
            ServiceProvider = serviceProvider;
            Metadata = propertyMetadata;
            MetadatumType = metadatumType;
        }

        public Type MetadatumType { get; }

        public IPropertyMetadata Metadata { get; }

        public IServiceProvider ServiceProvider { get; }
    }
}
