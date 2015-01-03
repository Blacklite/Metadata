﻿using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    internal interface ITypeMetadataInternal
    {
        ITypeMetadatumResolutionContext GetResolutionContext(Type type);
    }

    public interface ITypeMetadata : IMetadata
    {
        string Name { get; }
        Type Type { get; }
        TypeInfo TypeInfo { get; }

        IEnumerable<IPropertyMetadata> Properties { get; }
    }

    class TypeMetadata : ITypeMetadata, ITypeMetadataInternal
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

            _metadatumResolvers = metadatumResolverProvider.TypeResolvers;

            Type = type;

            TypeInfo = type.GetTypeInfo();
        }

        public virtual ITypeMetadatumResolutionContext GetResolutionContext(Type type)
        {
            return new TypeMetadatumResolutionContext(this, type, null);
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
                IEnumerable<ITypeMetadatumResolver> values;
                if (_metadatumResolvers.TryGetValue(typeof(T), out values))
                {
                    var context = GetResolutionContext(typeof(T));
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
    }
}
