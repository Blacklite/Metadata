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

    class OrderPropertyMetadatumResolver : SimplePropertyMetadatumResolver<Order>
    {
        private const int DisplayOrderDefault = 10000;

        public override Order Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var displayAttribute = context.Metadata.Attributes
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (displayAttribute != null)
            {
                return new Order(displayAttribute.Order);
            }

            var displayOrder = context.Metadata.Attributes
                .OfType<OrderAttribute>()
                .SingleOrDefault()?.Order ?? GetRelativePosition(context.Metadata);

            return new Order(displayOrder);
        }

        private static int CalculateDisplayOrder(PropertyInfo propertyInfo)
        {
            var delcaringTypeProperties = propertyInfo.DeclaringType.GetTypeInfo().DeclaredProperties;
            var propertiesOnDeclaringType = delcaringTypeProperties.Where(x => x.DeclaringType == propertyInfo.DeclaringType);
            var index = propertiesOnDeclaringType.Select((x, i) => x.Name == propertyInfo.Name ? i : -1).FirstOrDefault(x => x > -1);
            var numberOfPropertiesAboveDeclaringType = delcaringTypeProperties.Where(x => x.DeclaringType != propertyInfo.DeclaringType).Count();

            return index + 1 + numberOfPropertiesAboveDeclaringType;
        }

        public virtual int GetRelativePosition(IPropertyMetadata metadata)
        {
            var property = metadata.ParentMetadata.Type.GetTypeInfo().DeclaredProperties.SingleOrDefault(x => x.Name == metadata.Name);

            if (property != null)
                return CalculateDisplayOrder(property);

            return 0;
        }
    }
}