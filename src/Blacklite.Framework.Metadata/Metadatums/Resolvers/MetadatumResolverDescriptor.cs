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
        private readonly Func<IServiceProvider, IMetadatumResolutionContext<TMetadata>, IMetadatum> _resolveFunc;

        public TResolver Resolver { get; }

        public bool IsGlobal { get; }

        public Type MetadatumType { get; }

        public int Priority { get; }

        public MetadatumResolverDescriptor(TResolver resolver)
        {
            Resolver = resolver;

            var resolveMethod = resolver.GetType().GetTypeInfo().DeclaredMethods.SingleOrDefault(x => x.Name == nameof(Resolve));

            MetadatumType = resolver.GetMetadatumType();
            IsGlobal = MetadatumType == null;
            Priority = resolver.Priority;

            var contextTypeInfo = typeof(IMetadatumResolutionContext<TMetadata>).GetTypeInfo();

            _resolveFunc = resolveMethod.CreateInjectableMethod()
                .ConfigureParameter(x => contextTypeInfo.IsAssignableFrom(x.ParameterType.GetTypeInfo()))
                .ReturnType(typeof(IMetadatum))
                .CreateFunc<IMetadatumResolutionContext<TMetadata>, IMetadatum>(resolver);
        }

        public bool CanResolve(IMetadatumResolutionContext<TMetadata> context)
        {
            return Resolver.CanResolve(context);
        }

        public IMetadatum Resolve(IMetadatumResolutionContext<TMetadata> context)
        {
            return _resolveFunc(context.ServiceProvider, context);
        }
    }

}
