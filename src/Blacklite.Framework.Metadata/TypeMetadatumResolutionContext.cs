using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadatumResolutionContext
    {
        ITypeMetadata Metadata { get; }

        Type MetadatumType { get; }

        HttpContext HttpContext { get; }
    }

    class TypeMetadatumResolutionContext : ITypeMetadatumResolutionContext
    {
        public TypeMetadatumResolutionContext(ITypeMetadata typeMetadata, Type metadatumType, HttpContext httpContext)
        {
            Metadata = typeMetadata;
            MetadatumType = metadatumType;
            HttpContext = httpContext;
        }

        public HttpContext HttpContext { get; }

        public Type MetadatumType { get; }

        public ITypeMetadata Metadata { get; }
    }
}
