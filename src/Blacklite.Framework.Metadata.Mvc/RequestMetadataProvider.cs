using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Mvc
{
    class RequestMetadataProvider : MetadataProvider
    {
        private readonly HttpContext _httpContext;

        public RequestMetadataProvider(IHttpContextAccessor httpContext, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
            : base(metadataPropertyProvider, metadatumResolverProvider)
        {
            _httpContext = httpContext.Value;
        }

        protected override ITypeMetadata CreateTypeMetadata(Type type, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            return new TypeMetadata(type, _httpContext, metadataPropertyProvider, metadatumResolverProvider);
        }
    }
}
