using Microsoft.Framework.DependencyInjection;
using Blacklite.Framework.Metadata.Properties;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public class ScopedMetadataTypeMetadatumResolver : TypeMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context) =>
            context.ServiceProvider.GetService<IScopedMetadataContainer>()?.Has(context.Metadata, context.MetadatumType) ?? false;

        public IMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context, IScopedMetadataContainer container)
             => container.Get(context.Metadata, context.MetadatumType);
    }

    public class ScopedMetadataPropertyMetadatumResolver : PropertyMetadatumResolver<IMetadatum>
    {
        public override Type GetMetadatumType() => null;

        // This resolver is top priority, as it should only contain metadata that pertains to the current request.
        public override int Priority { get; } = int.MaxValue;

        public override bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context) =>
            context.ServiceProvider.GetService<IScopedMetadataContainer>()?.Has(context.Metadata, context.MetadatumType) ?? false;

        public IMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context, IScopedMetadataContainer container)
            => container.Get(context.Metadata, context.MetadatumType);
    }
}
