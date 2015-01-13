using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Blacklite.Framework.Shared.Reflection;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    class MetadatumResolverDescriptor<TResolver, TMetadata> : IMetadatumResolverDescriptor<TMetadata>
        where TMetadata : IMetadata
        where TResolver : IMetadatumResolver<TMetadata>
    {
        public TResolver Resolver { get; }

        private readonly IDictionary<Type, Func<IServiceProvider, IMetadatumResolutionContext<TMetadata>, object>> _resolversCache = new Dictionary<Type, Func<IServiceProvider, IMetadatumResolutionContext<TMetadata>, object>>();
        private readonly MethodInfo _resolveMethod;

        public bool IsGlobal { get; }

        public Type MetadatumType { get; }

        public int Priority { get; }

        public MetadatumResolverDescriptor(TResolver resolver)
        {
            Resolver = resolver;
            var typeInfo = Resolver.GetType().GetTypeInfo();

            _resolveMethod = typeInfo.DeclaredMethods.Single(x => x.Name == nameof(Resolve));

            MetadatumType = resolver.GetMetadatumType();
            IsGlobal = MetadatumType == null;
            Priority = resolver.Priority;
        }

        public bool CanResolve<T>(IMetadatumResolutionContext<TMetadata> context) where T : class, IMetadatum
        {
            return Resolver.CanResolve<T>(context);
        }

        public T Resolve<T>(IMetadatumResolutionContext<TMetadata> context) where T : class, IMetadatum
        {
            Func<IServiceProvider, IMetadatumResolutionContext<TMetadata>, object> method;
            if (!_resolversCache.TryGetValue(typeof(T), out method))
            {
                var genericMethod = _resolveMethod.MakeGenericMethod(typeof(T));
                var contextTypeInfo = typeof(IMetadatumResolutionContext<TMetadata>).GetTypeInfo();

                method = _resolveMethod.CreateInjectableMethod()
                    .ConfigureParameter(x => contextTypeInfo.IsAssignableFrom(x.ParameterType.GetTypeInfo()))
                    .CreateFunc<IMetadatumResolutionContext<TMetadata>, object>(Resolver);

                _resolversCache.Add(typeof(T), method);
            }

            return (T)method(context.ServiceProvider, context);
        }
    }

}
