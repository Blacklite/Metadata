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

    class ShortNameTypeMetadatumResolver : SimpleTypeMetadatumResolver<ShortName>
    {
        public override ShortName Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            var shortName = context.Metadata.TypeInfo
                .CustomAttributes.OfType<ShortNameAttribute>()
                .SingleOrDefault()?.ShortName ?? context.Metadata.Get<DisplayName>()?.Value;

            return new ShortName(shortName);
        }
    }
}