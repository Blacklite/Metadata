using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IMetadataProvider
    {
        ITypeMetadata GetMetadata<T>();

        ITypeMetadata GetMetadata(Type type);

        ITypeMetadata GetMetadata(TypeInfo typeInfo);
    }
}
