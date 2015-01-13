#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IApplicationMetadataProvider
    {
        IApplicationTypeMetadata GetMetadata<T>();

        IApplicationTypeMetadata GetMetadata(Type type);

        IApplicationTypeMetadata GetMetadata(TypeInfo typeInfo);
    }
}
