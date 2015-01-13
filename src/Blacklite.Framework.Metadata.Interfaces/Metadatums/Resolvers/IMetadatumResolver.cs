#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadatumResolver
    {
        Type GetMetadatumType();
        int Priority { get; }
    }

#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadatumResolver<TMetadata> : IMetadatumResolver
        where TMetadata : IMetadata
    {
        bool CanResolve<T>(IMetadatumResolutionContext<TMetadata> context) where T : class, IMetadatum;
    }

#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadatumResolver<TMetadata, TMetadatum> : IMetadatumResolver<TMetadata>
        where TMetadata : IMetadata
        where TMetadatum : class, IMetadatum
    {
    }
}
