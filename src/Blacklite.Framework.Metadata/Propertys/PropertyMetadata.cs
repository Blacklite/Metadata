using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Properties
{
    class PropertyMetadata : IPropertyMetadata, IInternalMetadata
    {
        private readonly IPropertyMetadata _parent;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();

        public PropertyMetadata(IPropertyMetadata parent, ITypeMetadata parentMetadata, IServiceProvider serviceProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _parent = parent;
            _metadatumResolverProvider = metadatumResolverProvider;
            _serviceProvider = serviceProvider;
            Name = parent.Name;
            ParentMetadata = parentMetadata;

            PropertyType = parent.PropertyType;
            PropertyInfo = parent.PropertyInfo;
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public string Key => string.Format("{0}@Property:{1}", ParentMetadata.ToString(), Name);

        public T GetValue<T>(object context) => _parent.GetValue<T>(context);

        public void SetValue<T>(object context, T value) => _parent.SetValue(context, value);

        public T Get<T>() where T : class, IMetadatum
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

        void IInternalMetadata.InvalidateMetadatumCache(Type type)
        {
            IMetadatum value;
            _metadatumCache.TryRemove(type, out value);
        }
    }
}
