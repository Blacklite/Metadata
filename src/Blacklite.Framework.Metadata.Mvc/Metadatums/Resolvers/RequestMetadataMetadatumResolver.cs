using Microsoft.Framework.DependencyInjection;
using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;

namespace Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers
{
    public class RequestMetadataTypeMetadatumResolver : TypeMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context) =>
            context.ServiceProvider.GetService<IRequestMetadataContainer>()?.Has(context.Metadata, context.MetadatumType) ?? false;

        public T Resolve<T>(IMetadatumResolutionContext<ITypeMetadata> context, IRequestMetadataContainer container)
            where T : IMetadatum => container.Get<T>(context.Metadata);
    }

    public class RequestMetadataPropertyMetadatumResolver : PropertyMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        // This resolver is top priority, as it should only contain metadata that pertains to the current request.
        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context) =>
            context.ServiceProvider.GetService<IRequestMetadataContainer>()?.Has(context.Metadata, context.MetadatumType) ?? false;

        public T Resolve<T>(IMetadatumResolutionContext<IPropertyMetadata> context, IRequestMetadataContainer container)
            where T : IMetadatum => container.Get<T>(context.Metadata);
    }
}
