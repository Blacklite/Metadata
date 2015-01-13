#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface ITypeMetadatumResolver : IMetadatumResolver<ITypeMetadata>, IMetadatumResolver { }

#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface ITypeMetadatumResolver<TMetadatum> : IMetadatumResolver<ITypeMetadata, TMetadatum>, ITypeMetadatumResolver where TMetadatum : class, IMetadatum { }
}
