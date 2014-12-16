using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.MetadataProperties
{
    class ReflectionPropertyDescriptor : IPropertyDescriptor
    {
        public IEnumerable<IPropertyDescriber> Describe(Type type)
        {
            return type.GetTypeInfo()
                .DeclaredProperties
                .Select(x => new PropertyDescriber()
                {
                    Name = x.Name,
                    PropertyType = x.PropertyType,
                    PropertyInfo = x.PropertyType.GetTypeInfo(),
                    Order = 0,
                    GetValue = x.GetValue,
                    SetValue = x.SetValue
                });
        }
    }
}
