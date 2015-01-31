using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IPropertyMetadata : IMetadata
    {
        ITypeMetadata ParentMetadata { get; }

        string Name { get; }

        IEnumerable<Attribute> Attributes { get; }

        Type PropertyType { get; }

        TypeInfo PropertyInfo { get; }

        T GetValue<T>(object context);

        void SetValue<T>(object context, T value);
    }
}
