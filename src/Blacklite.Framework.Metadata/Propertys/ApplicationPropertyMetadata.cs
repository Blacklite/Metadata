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
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public string Key => string.Format("{0}@Property:{1}", ParentMetadata.ToString(), Name);

        public T GetValue<T>(object context) => (T)_getValue(context);

        public void SetValue<T>(object context, T value) => _setValue(context, value);

        public T Get<T>() where T : IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>> values;
                if (_metadatumResolverProvider.PropertyResolvers.TryGetValue(typeof(T), out values))
                {
                    var context = new PropertyMetadatumResolutionContext(_serviceProvider, this, typeof(T));
                    var resolvedValue = values
                        .Where(z => z.CanResolve<T>(context))
                        .Select(x => x.Resolve<T>(context))
                        .FirstOrDefault(x => x != null);

                    if (resolvedValue != null)
                    {
                        _metadatumCache.TryAdd(typeof(T), resolvedValue);
                        return resolvedValue;
                    }
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
