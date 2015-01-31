using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class RegularExpression : IMetadatum
    {
        public RegularExpression(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }
    }
}
