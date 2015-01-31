using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.Framework.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    class InfoTypePropertyMetadatumResolver : SimplePropertyMetadatumResolver<InfoType>
    {
        public override InfoType Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var infoType = context.Metadata.Attributes
                .OfType<DataTypeAttribute>()
                .SingleOrDefault()?.DataType;

            if (infoType.HasValue)
            {
                return new InfoType(infoType.Value);
            }

            return null;
        }
    }
}
