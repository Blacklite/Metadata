using System;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IPropertyMetadata : IMetadata
    {
        ITypeMetadata ParentMetadata { get; }

        string Name { get; }

        Type PropertyType { get; }

        TypeInfo PropertyInfo { get; }
    }
}
