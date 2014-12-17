using Blacklite.Framework.Metadata.MetadataProperties;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IPropertyMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(IPropertyMetadata metadata);
        bool CanResolve(IPropertyMetadata metadata);
    }

    public interface IPropertyMetadatumResolver<T> : IPropertyMetadatumResolver
         where T : IMetadatum
    {
    }

    public abstract class PropertyMetadatumResolver<TProperty> : IPropertyMetadatumResolver<TProperty>
        where TProperty : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TProperty);

        public abstract T Resolve<T>(IPropertyMetadata metadata);

        public virtual bool CanResolve(IPropertyMetadata metadata) => true;
    }
}
