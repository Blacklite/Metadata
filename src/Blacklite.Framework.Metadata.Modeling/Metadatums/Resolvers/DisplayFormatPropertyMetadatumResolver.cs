using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    public class DisplayFormatPropertyMetadatumResolver : SimplePropertyMetadatumResolver<DisplayFormat>
    {
        public override DisplayFormat Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var attribute = context.Metadata.Attributes
                .OfType<DisplayFormatAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                return new DisplayFormat(attribute.DataFormatString, attribute.HtmlEncode);
            }

            return null;
        }
    }
}