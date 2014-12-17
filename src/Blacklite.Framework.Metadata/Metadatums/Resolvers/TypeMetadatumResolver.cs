using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface ITypeMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(ITypeMetadata metadata);
        bool CanResolve(ITypeMetadata metadata);
    }

    public interface ITypeMetadatumResolver<T> : ITypeMetadatumResolver
        where T : IMetadatum
    {
    }

    public abstract class TypeMetadatumResolver<TObject> : ITypeMetadatumResolver<TObject>
        where TObject : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TObject);

        public abstract T Resolve<T>(ITypeMetadata metadata);

        public virtual bool CanResolve(ITypeMetadata metadata) => true;
    }

}
