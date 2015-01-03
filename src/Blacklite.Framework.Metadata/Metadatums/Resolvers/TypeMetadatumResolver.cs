using Microsoft.AspNet.Http;
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface ITypeMetadatumResolver : IMetadatumResolver
    {
        T Resolve<T>(ITypeMetadatumResolutionContext context) where T : class, IMetadatum;
        bool CanResolve<T>(ITypeMetadatumResolutionContext context) where T : class, IMetadatum;
    }

    public interface ITypeMetadatumResolver<T> : ITypeMetadatumResolver
        where T : IMetadatum
    {
    }

    public abstract class TypeMetadatumResolver<TMetadatum> : ITypeMetadatumResolver<TMetadatum>
        where TMetadatum : IMetadatum
    {
        public abstract Type GetMetadatumType();

        public abstract int Priority { get; }

        public abstract T Resolve<T>(ITypeMetadatumResolutionContext context) where T : class, IMetadatum;

        public abstract bool CanResolve<T>(ITypeMetadatumResolutionContext context) where T : class, IMetadatum;
    }
}
