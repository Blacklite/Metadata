using System;
using System.Collections.Generic;
using System.Linq;
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

    class TypeMetadata : ITypeMetadata
    {
        public TypeMetadata(Type type, IPropertyMetadataProvider metadataPropertyProvider)
        {
            // how does this work??
            // All values are resolved from a caching interface of some sort
            // The cache interface draws from both the "application" level, but also the "scope" level.
            // This interface could be replaced to offer customization of this process, ie pull from the "applicaiton", "tennat" and "scope" levels.
            // Properties will come from a provider, that takes in the type, so that other properties can be generated at runtime.
            Name = type.Name;

            Properties = metadataPropertyProvider.GetProperties(this);

            Type = type;

            TypeInfo = type.GetTypeInfo();
        }

        public string Name { get; }

        public IEnumerable<IPropertyMetadata> Properties { get; }

        public Type Type { get; }

        public TypeInfo TypeInfo { get; }

        public T Get<T>() where T : IMetadatum
        {
            throw new NotImplementedException();
        }
    }
}
