using Blacklite.Framework.Metadata.MetadataProperties;
using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IPropertyMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(IPropertyMetadatumResolutionContext context) where T : class, IMetadatum;
        bool CanResolve<T>(IPropertyMetadatumResolutionContext context) where T : class, IMetadatum;
    }

    public interface IPropertyMetadatumResolver<T> : IPropertyMetadatumResolver
         where T : IMetadatum
    {
    }

    public abstract class PropertyMetadatumResolver<TProperty> : IPropertyMetadatumResolver<TProperty>
        where TProperty : IMetadatum
    {
        public abstract Type GetMetadatumType();

        public abstract int Priority { get; }

        public abstract T Resolve<T>(IPropertyMetadatumResolutionContext context) where T : class, IMetadatum;

        public abstract bool CanResolve<T>(IPropertyMetadatumResolutionContext context) where T : class, IMetadatum;
    }
}
