using Blacklite.Framework.Metadata.MetadataProperties;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IPropertyMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(IPropertyMetadata metadata) where T : class, IMetadatum;
        bool CanResolve<T>(IPropertyMetadata metadata) where T : class, IMetadatum;
    }

    public interface IPropertyMetadatumResolver<T> : IPropertyMetadatumResolver
         where T : IMetadatum
    {
    }

    public abstract class PropertyMetadatumResolver<TProperty> : IPropertyMetadatumResolver<TProperty>
        where TProperty : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TProperty);

        public abstract int Priority { get; }

        public abstract T Resolve<T>(IPropertyMetadata metadata) where T : class, IMetadatum;

        public virtual bool CanResolve<T>(IPropertyMetadata metadata) where T : class, IMetadatum => true;
    }
}
