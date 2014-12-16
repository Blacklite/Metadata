using Blacklite.Framework.Metadata.MetadataProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IPropertyMetadata : IMetadata
    {
        ITypeMetadata ParentMetadata { get; }

        string Name { get; }

        Type PropertyType { get; }

        TypeInfo PropertyInfo { get; }

        T GetValue<T>(object context);

        void SetValue<T>(object context, T value);
    }

    public class PropertyMetadata : IPropertyMetadata
    {
        private readonly IPropertyDescriber _propertyDescriber;
        private readonly Func<object, object> _getValue;
        private readonly Action<object, object> _setValue;

        public PropertyMetadata(ITypeMetadata parentMetadata, IPropertyDescriber propertyDescriber)
        {
            Name = propertyDescriber.Name;

            ParentMetadata = parentMetadata;

            PropertyType = propertyDescriber.PropertyType;
            PropertyInfo = propertyDescriber.PropertyInfo;

            _getValue = propertyDescriber.GetValue;
            _setValue = propertyDescriber.SetValue;

            _propertyDescriber = propertyDescriber;
        }

        public string Name { get; }

        public ITypeMetadata ParentMetadata { get; }

        public TypeInfo PropertyInfo { get; }

        public Type PropertyType { get; }

        public T GetValue<T>(object context) => (T)_getValue(context);

        public void SetValue<T>(object context, T value) => _setValue(context, value);

        public T Get<T>() where T : IMetadatum
        {
            throw new NotImplementedException();
        }
    }
}
