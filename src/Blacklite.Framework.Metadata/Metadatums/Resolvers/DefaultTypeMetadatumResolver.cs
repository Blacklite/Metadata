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

        T ITypeMetadatumResolver.Resolve<T>(ITypeMetadata metadata)
        {
            return Resolve(metadata) as T;
        }

        bool ITypeMetadatumResolver.CanResolve<T>(ITypeMetadata metadata)
        {
            if (typeof(T) == typeof(TMetadatum)) return CanResolve(metadata);

            return false;
        }
    }
}
