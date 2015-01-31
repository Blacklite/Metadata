using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    public class ShowForEditPropertyMetadatumResolver : SimplePropertyMetadatumResolver<ShowForEdit>
    {
        public override ShowForEdit Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var attribute = context.Metadata.Attributes
                .OfType<ShowForEditAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                return new ShowForEdit(attribute.Visible);
            }

            return null;
        }
    }
}