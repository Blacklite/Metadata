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
    class GroupTypeMetadatumResolver : SimpleTypeMetadatumResolver<Group>
    {
        public override Group Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            var groups = context.Metadata.TypeInfo.CustomAttributes
                .OfType<GroupAttribute>()
                .SelectMany(x => x.Groups)
                .ToArray();

            return new Group(groups);
        }
    }
}