using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public abstract class SimpleMetadatum<T> : IMetadatum
    {
        public SimpleMetadatum(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public static implicit operator T(SimpleMetadatum<T> description)
        {
            if (description == null)
                return default(T);

            return description.Value;
        }
    }
}