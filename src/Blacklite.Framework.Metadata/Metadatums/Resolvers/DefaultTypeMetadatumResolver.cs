using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public abstract class SimpleTypeMetadatumResolver<TMetadatum> : ITypeMetadatumResolver<TMetadatum>, IApplicationTypeMetadatumResolver<TMetadatum>
         where TMetadatum : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TMetadatum);

        public virtual int Priority { get; } = 0;

        public virtual bool CanResolve(ITypeMetadata metadata) => true;

        bool IMetadatumResolver<ITypeMetadata>.CanResolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            if (context.MetadatumType == typeof(TMetadatum))
                return CanResolve(context.Metadata);

            return false;
        }

        public abstract TMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context);
    }
}
