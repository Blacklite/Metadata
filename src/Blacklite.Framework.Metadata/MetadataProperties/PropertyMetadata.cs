using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.MetadataProperties
{
    class PropertyMetadata : IPropertyMetadata
    {
        private readonly IPropertyDescriber _propertyDescriber;
        private readonly Func<object, object> _getValue;
        private readonly Action<object, object> _setValue;
        private readonly IReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>> _metadatumResolvers;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();

        public PropertyMetadata(ITypeMetadata parentMetadata, IPropertyDescriber propertyDescriber, IMetadatumResolverProvider metadatumResolverProvider)
        {
            Name = propertyDescriber.Name;

            ParentMetadata = parentMetadata;

            PropertyType = propertyDescriber.PropertyType;
            PropertyInfo = propertyDescriber.PropertyInfo;

            _getValue = propertyDescriber.GetValue;
            _setValue = propertyDescriber.SetValue;

            _propertyDescriber = propertyDescriber;
            _metadatumResolvers = metadatumResolverProvider.GetPropertyResolvers();
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public T GetValue<T>(object context) => (T)_getValue(context);

        public void SetValue<T>(object context, T value) => _setValue(context, value);

        public T Get<T>() where T : class, IMetadatum
        {
            return (T)_metadatumCache.GetOrAdd(typeof(T), type =>
            {
                IEnumerable<IPropertyMetadatumResolver> values;
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
