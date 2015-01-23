using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface ITypeMetadata : IMetadata
    {
        string Name { get; }
        Type Type { get; }
        TypeInfo TypeInfo { get; }

        IEnumerable<IPropertyMetadata> Properties { get; }
    }
}
