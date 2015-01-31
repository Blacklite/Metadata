using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Properties
{
    class ApplicationPropertyMetadata : IPropertyMetadata
    {
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly IPropertyDescriber _propertyDescriber;
        private readonly Func<object, object> _getValue;
        private readonly Action<object, object> _setValue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();

        public ApplicationPropertyMetadata(ITypeMetadata parentMetadata, IPropertyDescriber propertyDescriber, IServiceProvider serviceProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            Name = propertyDescriber.Name;

            ParentMetadata = parentMetadata;

            PropertyType = propertyDescriber.PropertyType;
            PropertyInfo = propertyDescriber.PropertyInfo;

            _getValue = propertyDescriber.GetValue;
            _setValue = propertyDescriber.SetValue;

            _propertyDescriber = propertyDescriber;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;

            Attributes = propertyDescriber.Attributes ?? Enumerable.Empty<Attribute>();
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public string Key => string.Format("{0}@Property:{1}", ParentMetadata.ToString(), Name);

        public IEnumerable<Attribute> Attributes { get; }

        public T GetValue<T>(object context) => (T)_getValue(context);

        public void SetValue<T>(object context, T value) => _setValue(context, value);

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                var values = _metadatumResolverProvider.GetPropertyResolvers("Application", typeof(T));
                if (values != null)
                {
                    var context = new PropertyMetadatumResolutionContext(_serviceProvider, this, typeof(T));
                    var resolvedValue = values
                        .Where(z => z.CanResolve(context))
                        .Select(x => x.Resolve(context))
                        .FirstOrDefault(x => x != null);

                    _metadatumCache.TryAdd(typeof(T), resolvedValue);
                    return (T)resolvedValue;
                }
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
