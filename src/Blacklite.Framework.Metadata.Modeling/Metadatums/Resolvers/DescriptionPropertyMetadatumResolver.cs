using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.ComponentModel.DataAnnotations;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using System.Linq;
using System.Reflection;
using Microsoft.Framework.DependencyInjection;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{

    class DescriptionPropertyMetadatumResolver : SimplePropertyMetadatumResolver<Description>
    {
        public override Description Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var displayAttribute = context.Metadata.Attributes
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (displayAttribute != null)
            {
                return new Description(displayAttribute.Description);
            }

            var description = context.Metadata.Attributes
                .OfType<DescriptionAttribute>()
                .SingleOrDefault()?.Description;

            return new Description(description);
        }
    }
}