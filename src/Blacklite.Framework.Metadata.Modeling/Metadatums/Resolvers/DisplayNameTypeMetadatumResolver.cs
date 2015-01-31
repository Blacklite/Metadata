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

    class DisplayNameTypeMetadatumResolver : SimpleTypeMetadatumResolver<DisplayName>
    {
        public override DisplayName Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            var displayName = context.Metadata.TypeInfo
                .CustomAttributes.OfType<DisplayNameAttribute>()
                .SingleOrDefault()?.DisplayName ?? context.Metadata.Type.Name.AsUserFriendly();

            return new DisplayName(displayName);
        }
    }
}