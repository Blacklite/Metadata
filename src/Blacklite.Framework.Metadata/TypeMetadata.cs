using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    class TypeMetadata : ITypeMetadata
    {
        private readonly ITypeMetadata _fallback;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();
        private readonly IServiceProvider _serviceProvider;
        public TypeMetadata(ITypeMetadata fallback, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _fallback = fallback;
            _serviceProvider = serviceProvider;
            _metadatumResolverProvider = metadatumResolverProvider;

            Name = fallback.Name;
            Type = fallback.Type;
            TypeInfo = fallback.TypeInfo;
            Properties = metadataPropertyProvider.GetProperties(fallback, this, serviceProvider);
        }

        public string Name { get; }

        public IEnumerable<IPropertyMetadata> Properties { get; }

        public Type Type { get; }

        public TypeInfo TypeInfo { get; }

        public string Key => string.Format("Type:{0}", Type.FullName);

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>> values;
                IMetadatum resolvedValue = null;
                if (_metadatumResolverProvider.TypeResolvers.TryGetValue(typeof(T), out values))
                {
                    var context = new TypeMetadatumResolutionContext(_serviceProvider, this, typeof(T));
                    resolvedValue = values
                        .Where(z => z.CanResolve(context))
                        .Select(x => x.Resolve(context))
                        .FirstOrDefault(x => x != null);
                }

                if (resolvedValue == null)
                    resolvedValue = _fallback.Get<T>();

                if (resolvedValue != null)
                {
                    _metadatumCache.TryAdd(typeof(T), resolvedValue);
                    return (T)resolvedValue;
                }

                throw new ArgumentOutOfRangeException("T", "Metadatum type '{0}' must have at least one resolver registered.");
            }

            return (T)value;
        }

        public override string ToString() => Key;

        public bool InvalidateMetadatumCache(Type type)
        {
            IMetadatum value;
            return _metadatumCache.TryRemove(type, out value);
        }
    }
}
