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
        private readonly IEnumerable<IPropertyDescriber> _propertyDescribers;
        private readonly Func<object, object> _getValue;
        private readonly Action<object, object> _setValue;

        public PropertyMetadata(ITypeMetadata parentMetadata, IEnumerable<IPropertyDescriber> propertyDescribers)
        {
            Name = propertyDescribers.First(x => !string.IsNullOrWhiteSpace(x.Name))?.Name;

            ParentMetadata = parentMetadata;

            PropertyType = propertyDescribers.First(x => x.PropertyType != null)?.PropertyType;
            PropertyInfo = propertyDescribers.First(x => x.PropertyInfo != null)?.PropertyInfo;

            _getValue = propertyDescribers.First(x => x.PropertyInfo != null)?.GetValue;
            _setValue = propertyDescribers.FirstOrDefault(x => x.PropertyInfo != null)?.SetValue;

            _propertyDescribers = propertyDescribers;
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
