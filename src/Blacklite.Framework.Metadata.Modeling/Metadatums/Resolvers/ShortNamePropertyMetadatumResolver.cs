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

    class ShortNamePropertyMetadatumResolver : SimplePropertyMetadatumResolver<ShortName>
    {
        public override ShortName Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var displayAttribute = context.Metadata.Attributes
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (displayAttribute != null)
            {
                return new ShortName(displayAttribute.ShortName);
            }

            var shortName = context.Metadata.Attributes
                .OfType<ShortNameAttribute>()
                .SingleOrDefault()?.ShortName ?? context.Metadata.Get<DisplayName>()?.Value;

            return new ShortName(shortName);
        }
    }
}