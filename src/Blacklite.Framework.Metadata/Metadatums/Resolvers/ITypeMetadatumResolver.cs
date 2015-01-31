


using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface IScopedTypeMetadatumResolver : IMetadatumResolver<ITypeMetadata>, IMetadatumResolver { }

    public interface ITypeMetadatumResolver<TMetadatum> : IMetadatumResolver<ITypeMetadata, TMetadatum>, IScopedTypeMetadatumResolver where TMetadatum : IMetadatum { }
}
