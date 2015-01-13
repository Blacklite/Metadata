#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IPropertyMetadatumResolver : IMetadatumResolver<IPropertyMetadata>, IMetadatumResolver { }

#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IPropertyMetadatumResolver<TMetadatum> : IMetadatumResolver<IPropertyMetadata, TMetadatum>, IPropertyMetadatumResolver where TMetadatum : class, IMetadatum { }

}
