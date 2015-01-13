﻿using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    class ApplicationTypeMetadata : IApplicationTypeMetadata, IInternalMetadata
    {
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;
        private readonly ConcurrentDictionary<Type, IMetadatum> _metadatumCache = new ConcurrentDictionary<Type, IMetadatum>();
        private readonly IServiceProvider _serviceProvider;
        public ApplicationTypeMetadata(Type type, IServiceProvider serviceProvider, IPropertyMetadataProvider metadataPropertyProvider, IMetadatumResolverProvider metadatumResolverProvider)
        {
            _serviceProvider = serviceProvider;
            _metadatumResolverProvider = metadatumResolverProvider;
            // how does this work??
            // All values are resolved from a caching interface of some sort
            // The cache interface draws from both the "application" level, but also the "scope" level.
            // This interface could be replaced to offer customization of this process, ie pull from the "applicaiton", "tennat" and "scope" levels.
            // Properties will come from a provider, that takes in the type, so that other properties can be generated at runtime.
            Name = type.Name;
            Type = type;
            TypeInfo = type.GetTypeInfo();
            Properties = metadataPropertyProvider.GetApplicationProperties(this, serviceProvider);
        }

        public string Name { get; }

        public IEnumerable<IPropertyMetadata> Properties { get; }

        public Type Type { get; }

        public TypeInfo TypeInfo { get; }

        public string Key => string.Format("Type:{0}", Type.FullName);

        public T Get<T>() where T : class, IMetadatum
        {
            IMetadatum value;
            if (!_metadatumCache.TryGetValue(typeof(T), out value))
            {
                IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>> values;
                if (_metadatumResolverProvider.ApplicationTypeResolvers.TryGetValue(typeof(T), out values))
                {
                    var context = new TypeMetadatumResolutionContext(_serviceProvider, this, typeof(T));
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
