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
        private readonly IPropertyMetadata _fallback;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();

        public PropertyMetadata(IPropertyMetadata fallback, ITypeMetadata parentMetadata, IServiceProvider serviceProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _fallback = fallback;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;
            Name = fallback.Name;
            ParentMetadata = parentMetadata;

            PropertyType = fallback.PropertyType;
            PropertyInfo = fallback.PropertyInfo;
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public string Key => string.Format("{0}@Property:{1}", ParentMetadata.ToString(), Name);

        public T GetValue<T>(object context) => _fallback.GetValue<T>(context);

        public void SetValue<T>(object context, T value) => _fallback.SetValue(context, value);

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>> values;
                IMetadatum resolvedValue = null;
                if (_metadatumResolverProvider.PropertyResolvers.TryGetValue(typeof(T), out values))
                {
                    var context = new PropertyMetadatumResolutionContext(_serviceProvider, this, typeof(T));
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
