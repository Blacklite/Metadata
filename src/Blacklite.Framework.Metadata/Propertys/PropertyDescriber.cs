using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Properties
{
    class PropertyDescriber : IPropertyDescriber
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public TypeInfo PropertyInfo { get; set; }

        public Type PropertyType { get; set; }

        public Func<object, object> GetValue { get; set; }

        public Action<object, object> SetValue { get; set; }

        public IEnumerable<Attribute> Attributes { get; set; }
    }
}
