using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Blacklite.Framework.GlobalEvents;
using Blacklite.Framework.Events;

namespace Blacklite.Framework.Metadata.Lifetimes
{
    class LifetimeMetadataPropertyProvider : PropertyMetadataProvider
    {
        private readonly ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>> _describerCache = new ConcurrentDictionary<Type, IEnumerable<IPropertyDescriber>>();
        private readonly Func<IEnumerable<IPropertyDescriptor>> _propertyDescriptorsFunc;
        private IEnumerable<IPropertyDescriptor> _propertyDescriptors;
        private readonly IDisposable _disposable;

        public LifetimeMetadataPropertyProvider(
            IServiceProvider serviceProvider,
            Func<IEnumerable<IPropertyDescriptor>> propertyDescriptorsFunc,
            IEventObservable<IGlobalEvent> eventObservable,
            IMetadatumResolverProvider metadatumResolverProvider)
            : base(serviceProvider, Enumerable.Empty<IPropertyDescriptor>(), metadatumResolverProvider)
        {
            _propertyDescriptorsFunc = propertyDescriptorsFunc;

            _disposable = eventObservable
                .Where(x => x.Type == EventType.ResetMetadata.ToString() || x.Type == EventType.ResetCache.ToString())
                .Subscribe(x => ClearPropertyDescriptors());
        }

        protected override IEnumerable<IPropertyDescriptor> Descriptors { get { return _propertyDescriptors ?? (_propertyDescriptors = _propertyDescriptorsFunc()); } }

        public void ClearPropertyDescriptors()
        {
            // If property descriptors are runtime based, this lets us clear them
            // this prepares us for the possibility of multitenancy
            _propertyDescriptors = null;
            _describerCache.Clear();
        }
    }
}
