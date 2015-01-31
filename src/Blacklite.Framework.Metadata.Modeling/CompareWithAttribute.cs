using Blacklite.Framework.Metadata.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Modeling
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CompareWithAttribute : Attribute
    {
        public string Property { get; }
        public CompareOperator Operator { get; }

        public CompareWithAttribute(string property, CompareOperator compareOperator = CompareOperator.EqualTo)
        {
            Property = property;
            Operator = compareOperator;
        }
    }
}