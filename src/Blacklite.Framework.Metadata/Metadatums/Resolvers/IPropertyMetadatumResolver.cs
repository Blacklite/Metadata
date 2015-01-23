
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IPropertyMetadatumResolver : IMetadatumResolver<IPropertyMetadata>, IMetadatumResolver { }

    public interface IPropertyMetadatumResolver<TMetadatum> : IMetadatumResolver<IPropertyMetadata, TMetadatum>, IPropertyMetadatumResolver where TMetadatum : IMetadatum { }
}
