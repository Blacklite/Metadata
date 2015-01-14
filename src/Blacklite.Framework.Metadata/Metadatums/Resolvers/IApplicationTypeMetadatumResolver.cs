


using System;

namespace Blacklite.Framework.Metadata.Metadatums.Resolvers
{



    public interface IApplicationTypeMetadatumResolver : IMetadatumResolver<ITypeMetadata>, IMetadatumResolver { }




    public interface IApplicationTypeMetadatumResolver<TMetadatum> : IMetadatumResolver<ITypeMetadata, TMetadatum>, IApplicationTypeMetadatumResolver where TMetadatum : class, IMetadatum { }

}
