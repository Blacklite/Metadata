using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class DefaultTypeMetadatumResolver<TMetadatum> : ITypeMetadatumResolver<TMetadatum>
         where TMetadatum : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TMetadatum);

        public virtual int Priority { get; } = 0;

        public abstract TMetadatum Resolve(ITypeMetadata metadata);

        public virtual bool CanResolve(ITypeMetadata metadata) => true;

        T ITypeMetadatumResolver.Resolve<T>(ITypeMetadatumResolutionContext context)
        {
            return Resolve(context.Metadata) as T;
        }

        bool ITypeMetadatumResolver.CanResolve<T>(ITypeMetadatumResolutionContext context)
        {
            if (typeof(T) == typeof(TMetadatum)) return CanResolve(context.Metadata);

            return false;
        }
    }
}
