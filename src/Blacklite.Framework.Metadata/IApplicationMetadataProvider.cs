using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IApplicationMetadataProvider
    {
        IApplicationTypeMetadata GetMetadata<T>();

        IApplicationTypeMetadata GetMetadata(Type type);

        IApplicationTypeMetadata GetMetadata(TypeInfo typeInfo);
    }
}
