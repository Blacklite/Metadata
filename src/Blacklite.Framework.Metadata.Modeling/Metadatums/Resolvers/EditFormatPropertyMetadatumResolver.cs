using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    public class EditFormatPropertyMetadatumResolver : SimplePropertyMetadatumResolver<EditFormat>
    {
        public override EditFormat Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var attribute = context.Metadata.Attributes
                .OfType<EditFormatAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                return new EditFormat(attribute.DataFormatString, attribute.HtmlEncode);
            }

            return null;
        }
    }
}