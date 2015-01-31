using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using Blacklite.Framework.Metadata;
using System.Linq;
using Microsoft.Framework.DependencyInjection;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    class CompareMetadatumResolver : SimplePropertyMetadatumResolver<Compare>
    {
        public override Compare Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var attribute = context.Metadata.Attributes
                .OfType<CompareWithAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                return new Compare(attribute.Property, attribute.Operator);
            }

            return null;
        }
    }
}
