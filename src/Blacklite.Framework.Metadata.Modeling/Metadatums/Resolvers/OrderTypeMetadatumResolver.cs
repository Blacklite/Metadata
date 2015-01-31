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

    class OrderTypeMetadatumResolver : SimpleTypeMetadatumResolver<Order>
    {
        public override Order Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
        {
            var displayOrder = context.Metadata.TypeInfo
                .CustomAttributes.OfType<OrderAttribute>()
                .SingleOrDefault()?.Order ?? 0;

            return new Order(displayOrder);
        }
    }
}