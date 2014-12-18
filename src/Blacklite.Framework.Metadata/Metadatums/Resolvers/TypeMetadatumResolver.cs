using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface ITypeMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(ITypeMetadata metadata) where T : class, IMetadatum;
        bool CanResolve<T>(ITypeMetadata metadata) where T : class, IMetadatum;
    }

    public interface ITypeMetadatumResolver<T> : ITypeMetadatumResolver
        where T : IMetadatum
    {
    }

    public abstract class TypeMetadatumResolver<TObject> : ITypeMetadatumResolver<TObject>
        where TObject : IMetadatum
    {
        public virtual Type GetMetadatumType() => typeof(TObject);

        public abstract int Priority { get; }

        public abstract T Resolve<T>(ITypeMetadata metadata) where T : class, IMetadatum;

        public virtual bool CanResolve<T>(ITypeMetadata metadata) where T : class, IMetadatum => true;
    }

}
