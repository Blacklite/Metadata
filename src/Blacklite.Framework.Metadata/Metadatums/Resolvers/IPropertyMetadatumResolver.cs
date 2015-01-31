
using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IScopedPropertyMetadatumResolver : IMetadatumResolver<IPropertyMetadata>, IMetadatumResolver { }

    public interface IPropertyMetadatumResolver<TMetadatum> : IMetadatumResolver<IPropertyMetadata, TMetadatum>, IScopedPropertyMetadatumResolver where TMetadatum : IMetadatum { }
}
