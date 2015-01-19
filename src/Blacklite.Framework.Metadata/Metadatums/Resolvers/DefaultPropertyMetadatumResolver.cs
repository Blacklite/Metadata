using Blacklite.Framework.Metadata.Properties;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class SimplePropertyMetadatumResolver<TMetadatum> : IPropertyMetadatumResolver<TMetadatum>, IApplicationPropertyMetadatumResolver<TMetadatum>
         where TMetadatum : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TMetadatum);

        public virtual int Priority { get; } = 0;

        public virtual bool CanResolve(IPropertyMetadata metadata) => true;

        bool IMetadatumResolver<IPropertyMetadata>.CanResolve<T>(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            if (typeof(T) == typeof(TMetadatum))
                return CanResolve(context.Metadata);

            return false;
        }

        public abstract TMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context);
    }
}
