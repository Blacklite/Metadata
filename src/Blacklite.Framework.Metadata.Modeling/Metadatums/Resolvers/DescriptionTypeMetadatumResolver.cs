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

    class DescriptionTypeMetadatumResolver : SimpleTypeMetadatumResolver<Description>
    {
        public override Description Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            var description = context.Metadata.TypeInfo
                .CustomAttributes.OfType<DescriptionAttribute>()
                .SingleOrDefault()?.Description;

            return new Description(description);
        }
    }
}