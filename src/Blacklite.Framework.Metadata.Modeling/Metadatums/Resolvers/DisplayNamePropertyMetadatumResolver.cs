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

    class DisplayNamePropertyMetadatumResolver : SimplePropertyMetadatumResolver<DisplayName>
    {
        public override DisplayName Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var displayAttribute = context.Metadata.Attributes
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (displayAttribute != null)
            {
                return new DisplayName(displayAttribute.Name);
            }

            var displayName = context.Metadata.Attributes
                .OfType<DisplayNameAttribute>()
                .SingleOrDefault()?.DisplayName ?? context.Metadata.Name.AsUserFriendly();

            return new DisplayName(displayName);
        }
    }
}