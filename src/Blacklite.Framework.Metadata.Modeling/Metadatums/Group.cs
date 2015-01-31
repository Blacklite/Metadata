using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Group : IMetadatum
    {
        public Group(params string[] groups)
        {
            Value = groups;
        }

        public IEnumerable<string> Value { get; }

        public static implicit operator string[] (Group description)
        {
            return description.Value.ToArray();
        }
    }
}