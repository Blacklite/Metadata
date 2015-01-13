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
    public interface IPropertyDescriber
    {
        int Order { get; }

        string Name { get; }

        Type PropertyType { get; }

        TypeInfo PropertyInfo { get; }

        Func<object, object> GetValue { get; }

        Action<object, object> SetValue { get; }
    }
}
