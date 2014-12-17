using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Blacklite.Framework.Metadata.Lifecycles
{
    public class LifecycleMetadataPropertyProvider : IPropertyMetadataProvider, IDisposable
    {
        private readonly ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>> _describerCache = new ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>>();
        private readonly Func<IEnumerable<IPropertyDescriptor>> _propertyDescriptorsFunc;
        private IEnumerable<IPropertyDescriptor> _propertyDescriptors;
        private readonly IDisposable _disposable;
        private readonly IMetadatumResolverProvider _metadatumResolverProvider;

        public LifecycleMetadataPropertyProvider(
            Func<IEnumerable<IPropertyDescriptor>> propertyDescriptorsFunc,
            IEventObservable eventObservable,
            IMetadatumResolverProvider metadatumResolverProvider)
        {
            _propertyDescriptorsFunc = propertyDescriptorsFunc;
            _metadatumResolverProvider = metadatumResolverProvider;

            _disposable = eventObservable
                .Where(x => x.Type == EventType.ResetMetadata.ToString() || x.Type == EventType.ResetCache.ToString())
                .Subscribe(x => ClearPropertyDescriptors());
        }

        public IEnumerable<IPropertyMetadata> GetProperties(ITypeMetadata parentMetadata) =>
            _describerCache.GetOrAdd(parentMetadata.Type, type =>
                (_propertyDescriptors ?? (_propertyDescriptors = _propertyDescriptorsFunc()))
                    .SelectMany(x => x.Describe(type))
                    .GroupBy(x => x.Name)
                    .Select(x => x.OrderByDescending(z => z.Order).First())
                )
                .Select(x => new PropertyMetadata(parentMetadata, x, _metadatumResolverProvider));

        public void ClearPropertyDescriptors()
        {
            // If property descriptors are runtime based, this lets us clear them
            // this prepares us for the possibility of multitenancy
            _propertyDescriptors = null;
            _describerCache.Clear();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MetadataPropertyProvider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
