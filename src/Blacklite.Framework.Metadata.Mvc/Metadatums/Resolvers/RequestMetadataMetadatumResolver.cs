using Microsoft.Framework.DependencyInjection;
using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers
{
    static class RequestMetadataContainerExtension
    {
        public static IRequestMetadataContainer GetRequestMetadata(this HttpContext context)
        {
            if (!context.Items.ContainsKey(typeof(IRequestMetadataContainer)))
            {
                context.Items[typeof(IRequestMetadataContainer)] = context.RequestServices.GetService<IRequestMetadataContainer>();
            }
            return (IRequestMetadataContainer)context.Items[typeof(IRequestMetadataContainer)];
        }
    }

    public class RequestMetadataTypeMetadatumResolver : TypeMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve<T>(ITypeMetadatumResolutionContext context)
        {
            if (context.HttpContext == null)
                return false;

            return context.HttpContext.GetRequestMetadata().Has<T>(context.Metadata);
        }

        public override T Resolve<T>(ITypeMetadatumResolutionContext context) =>
            context.HttpContext.GetRequestMetadata().Get<T>(context.Metadata);
    }

    public class RequestMetadataPropertyMetadatumResolver : PropertyMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        // This resolver is top priority, as it should only contain metadata that pertains to the current request.
        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve<T>(IPropertyMetadatumResolutionContext context)
        {
            if (context.HttpContext == null)
                return false;

            return context.HttpContext.GetRequestMetadata().Has<T>(context.Metadata);
        }

        public override T Resolve<T>(IPropertyMetadatumResolutionContext context) =>
            context.HttpContext.GetRequestMetadata().Get<T>(context.Metadata);
    }
}
