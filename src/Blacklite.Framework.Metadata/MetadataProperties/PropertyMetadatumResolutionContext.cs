using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.MetadataProperties
{
    public interface IPropertyMetadatumResolutionContext
    {
        IPropertyMetadata Metadata { get; }

        Type MetadatumType { get; }

        HttpContext HttpContext { get; }
    }

    class PropertyMetadatumResolutionContext : IPropertyMetadatumResolutionContext
    {
        public PropertyMetadatumResolutionContext(IPropertyMetadata propertyMetadata, Type metadatumType, HttpContext httpContext)
        {
            Metadata = propertyMetadata;
            MetadatumType = metadatumType;
            HttpContext = httpContext;
        }

        public HttpContext HttpContext { get; }

        public Type MetadatumType { get; }

        public IPropertyMetadata Metadata { get; }
    }
}
