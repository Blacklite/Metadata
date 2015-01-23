using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IApplicationPropertyMetadatumResolver : IMetadatumResolver<IPropertyMetadata>, IMetadatumResolver { }
    
    public interface IApplicationPropertyMetadatumResolver<TMetadatum> : IMetadatumResolver<IPropertyMetadata, TMetadatum>, IApplicationPropertyMetadatumResolver where TMetadatum : IMetadatum { }
}
