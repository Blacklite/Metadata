using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
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
