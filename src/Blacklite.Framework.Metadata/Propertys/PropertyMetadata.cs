using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Properties
{
    class PropertyMetadata : IPropertyMetadata
    {
        private readonly string _key;
        private readonly IPropertyMetadata _fallback;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();

        public PropertyMetadata(IPropertyMetadata fallback, string key, ITypeMetadata parentMetadata, IServiceProvider serviceProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _key = key;
            _fallback = fallback;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;
            Name = fallback.Name;
            ParentMetadata = parentMetadata;

            PropertyType = fallback.PropertyType;
            PropertyTypeInfo = fallback.PropertyTypeInfo;
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyTypeInfo { get; }

        public Type PropertyType { get; }

        public string Key => string.Format("{0}@Property:{1}", ParentMetadata.ToString(), Name);

        public T GetValue<T>(object context) => _fallback.GetValue<T>(context);

        public void SetValue<T>(object context, T value) => _fallback.SetValue(context, value);

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IMetadatum resolvedValue = null;
                var values = _metadatumResolverProvider.GetPropertyResolvers(_key, typeof(T));
                if (values != null)
                {
                    var context = new PropertyMetadatumResolutionContext(_serviceProvider, this, typeof(T));
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
