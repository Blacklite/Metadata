using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.Framework.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    class RequiredPropertyMetadatumResolver : SimplePropertyMetadatumResolver<Required>
    {
        public override Required Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var requiredAttribute = context.Metadata.Attributes
                .OfType<RequiredAttribute>()
                .SingleOrDefault();

            if (requiredAttribute != null)
                return new Required(true);

            return new Required(false);
        }
    }
}
