using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Concurrent;

namespace Blacklite.Framework.Metadata
{
    class InMemoryMetadataContainer : IMetadataContainer
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, IMetadatum>> _store = new ConcurrentDictionary<string, ConcurrentDictionary<Type, IMetadatum>>();

        private ConcurrentDictionary<Type, IMetadatum> GetStore(IMetadata parentMetadata)
        {
            return _store.GetOrAdd(parentMetadata.Key, x => new ConcurrentDictionary<Type, IMetadatum>());
        }

        public IMetadatum Get(IMetadata parentMetadata, Type type)
        {
            IMetadatum value;
            if (GetStore(parentMetadata).TryGetValue(type, out value))
                return value;

            throw new IndexOutOfRangeException("Could not get value for metadatum that does not exist in the store.");
        }

        public T Get<T>(IMetadata parentMetadata) where T : IMetadatum => (T)Get(parentMetadata, typeof(T));

        public bool Has(IMetadata parentMetadata, Type type) => GetStore(parentMetadata).ContainsKey(type);

        public bool Has<T>(IMetadata parentMetadata) where T : IMetadatum => Has(parentMetadata, typeof(T));

        public void Save(IMetadata parentMetadata, Type type, IMetadatum value) => GetStore(parentMetadata).TryAdd(type, value);

        public void Save<T>(IMetadata parentMetadata, T value) where T : IMetadatum => Save(parentMetadata, typeof(T), value);
    }
}
