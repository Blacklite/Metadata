


using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{
    public interface ITypeMetadatumResolver : IMetadatumResolver<ITypeMetadata>, IMetadatumResolver { }

    public interface ITypeMetadatumResolver<TMetadatum> : IMetadatumResolver<ITypeMetadata, TMetadatum>, ITypeMetadatumResolver where TMetadatum : IMetadatum { }
}
