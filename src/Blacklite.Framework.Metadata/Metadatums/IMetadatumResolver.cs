using System;

namespace Blacklite.Framework.Metadata.Metadatums
{
    public interface IMetadatumResolver
    {
        Type GetMetadatumType();
    }

    public interface ITypeMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(ITypeMetadata metadata);
    }

    public interface IPropertyMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(IPropertyMetadata metadata);
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
    }
}
