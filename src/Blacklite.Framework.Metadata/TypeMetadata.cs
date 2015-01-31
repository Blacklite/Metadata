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
    public interface ITypeMetadata : IMetadata
    {
        string Name { get; }
        Type Type { get; }
        TypeInfo TypeInfo { get; }

        IEnumerable<IPropertyMetadata> Properties { get; }
    }

    class TypeMetadata : ITypeMetadata
    {
        private readonly string _key;
        private readonly ITypeMetadata _fallback;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();
        private readonly IServiceProvider _serviceProvider;
        public TypeMetadata(ITypeMetadata fallback, string key, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _key = key;
            _fallback = fallback;
            _serviceProvider = serviceProvider;
            _metadatumResolverProvider = metadatumResolverProvider;

            Name = fallback.Name;
            Type = fallback.Type;
            TypeInfo = fallback.TypeInfo;
            Properties = metadataPropertyProvider.GetProperties(fallback, this, _key, serviceProvider).ToArray();
        }

        public string Name { get; }

        public IEnumerable<IPropertyMetadata> Properties { get; }

        public Type Type { get; }

        public TypeInfo TypeInfo { get; }

        public string Key => string.Format("Type:{0}", Type.FullName);

        public IEnumerable<Attribute> Attributes { get; }

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IMetadatum resolvedValue = null;
                var values = _metadatumResolverProvider.GetTypeResolvers(_key, typeof(T));
                if (values != null)
                {
                    var context = new TypeMetadatumResolutionContext(_serviceProvider, this, typeof(T));
                    resolvedValue = values
                        .Where(z => z.CanResolve(context))
                        .Select(x => x.Resolve(context))
                        .FirstOrDefault(x => x != null);
                }

                if (resolvedValue == null)
                    resolvedValue = _fallback.Get<T>();

                _metadatumCache.TryAdd(typeof(T), resolvedValue);
                return (T)resolvedValue;
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
