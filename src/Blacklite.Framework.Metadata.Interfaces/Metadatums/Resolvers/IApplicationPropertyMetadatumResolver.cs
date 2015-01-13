#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IApplicationPropertyMetadatumResolver : IMetadatumResolver<IPropertyMetadata>, IMetadatumResolver { }

#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IApplicationPropertyMetadatumResolver<TMetadatum> : IMetadatumResolver<IPropertyMetadata, TMetadatum>, IApplicationPropertyMetadatumResolver where TMetadatum : class, IMetadatum { }
}
