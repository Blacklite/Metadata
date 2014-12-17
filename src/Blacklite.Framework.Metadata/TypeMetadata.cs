using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
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
        private readonly IReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>> _metadatumResolvers;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();
        public TypeMetadata(Type type, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            // how does this work??
            // All values are resolved from a caching interface of some sort
            // The cache interface draws from both the "application" level, but also the "scope" level.
            // This interface could be replaced to offer customization of this process, ie pull from the "applicaiton", "tennat" and "scope" levels.
            // Properties will come from a provider, that takes in the type, so that other properties can be generated at runtime.
            Name = type.Name;

            Properties = metadataPropertyProvider.GetProperties(this);

            _metadatumResolvers = metadatumResolverProvider.GetTypeResolvers();

            Type = type;

            TypeInfo = type.GetTypeInfo();
        }

        public string Name { get; }

        public IEnumerable<IPropertyMetadata> Properties { get; }

        public Type Type { get; }

        public TypeInfo TypeInfo { get; }

        public T Get<T>() where T : class, IMetadatum
        {
            return (T)_metadatumCache.GetOrAdd(typeof(T), type =>
            {
                IEnumerable<ITypeMetadatumResolver> values;
                if (_metadatumResolvers.TryGetValue(typeof(T), out values))
                {
                    var resolvedValue = values
                        .Where(z => z.CanResolve(this))
                        .Select(x => x.Resolve<T>(this))
                        .FirstOrDefault(x => x != null);

                    if (resolvedValue != null)
                    {
                        return resolvedValue;
                    }
                }

                throw new ArgumentOutOfRangeException("T", "Metadatum type '{0}' must have at least one resolver registered.");
            });
        }
    }
}
