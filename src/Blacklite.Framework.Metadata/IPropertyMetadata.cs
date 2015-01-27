using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IPropertyMetadata : IMetadata
    {
        ITypeMetadata ParentMetadata { get; }

        string Name { get; }

        Type PropertyType { get; }

        TypeInfo PropertyTypeInfo { get; }

        T GetValue<T>(object context);

        void SetValue<T>(object context, T value);
    }
}
