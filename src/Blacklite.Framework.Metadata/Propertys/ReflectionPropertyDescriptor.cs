using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Properties
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadOnlyAttribute : Attribute
    {
        public ReadOnlyAttribute(bool readOnly)
        {
            ReadOnly = readOnly;
        }

        public bool ReadOnly { get; }
    }

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
                    SetValue = x.SetValue,
                    Attributes = GetAttributes(x).ToArray()
                });
        }

        public IEnumerable<Attribute> GetAttributes(PropertyInfo propertyInfo)
        {
            foreach (var attribute in propertyInfo.GetCustomAttributes())
            {
                yield return attribute;
            }

            if (propertyInfo.CanWrite)
                yield return new ReadOnlyAttribute(true);
        }
    }
}
