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

    class GroupPropertyMetadatumResolver : SimplePropertyMetadatumResolver<Group>
    {
        public override Group Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var displayAttribute = context.Metadata.Attributes
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (displayAttribute != null)
            {
                return new Group(displayAttribute.GroupName);
            }

            var groups = context.Metadata.Attributes
                .OfType<GroupAttribute>()
                .SelectMany(x => x.Groups)
                .ToArray();

            return new Group(groups);
        }
    }
}