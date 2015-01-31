using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    public class ShowForDisplayPropertyMetadatumResolver : SimplePropertyMetadatumResolver<ShowForDisplay>
    {
        public override ShowForDisplay Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var attribute = context.Metadata.Attributes
                .OfType<ShowForDisplayAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                return new ShowForDisplay(attribute.Visible);
            }

            return null;
        }
    }
}