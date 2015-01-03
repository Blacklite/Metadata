using Blacklite.Framework.Metadata.MetadataProperties;
using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class DefaultPropertyMetadatumResolver<TMetadatum> : IPropertyMetadatumResolver<TMetadatum>
         where TMetadatum : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TMetadatum);

        public virtual int Priority { get; } = 0;

        public abstract TMetadatum Resolve(IPropertyMetadata metadata);

        public virtual bool CanResolve(IPropertyMetadata metadata) => true;

        T IPropertyMetadatumResolver.Resolve<T>(IPropertyMetadatumResolutionContext context)
        {
            return Resolve(context.Metadata) as T;
        }

        bool IPropertyMetadatumResolver.CanResolve<T>(IPropertyMetadatumResolutionContext context)
        {
            if (typeof(T) == typeof(TMetadatum)) return CanResolve(context.Metadata);

            return false;
        }
    }
}
