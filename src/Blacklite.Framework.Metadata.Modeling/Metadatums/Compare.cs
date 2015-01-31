using Blacklite.Framework.Metadata.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Compare : IMetadatum
    {
        public string Property { get; }
        public CompareOperator Operator { get; }

        public Compare(string property, CompareOperator compareOperator = CompareOperator.EqualTo)
        {
            Property = property;
            Operator = compareOperator;
        }
    }
}
