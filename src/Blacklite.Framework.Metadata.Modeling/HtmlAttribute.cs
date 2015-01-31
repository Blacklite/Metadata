using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling
{


    [AttributeUsage(AttributeTargets.Property)]
    public sealed class HtmlAttribute : DataTypeAttribute
    {
        public HtmlAttribute() : base(DataType.Html) { }
    }
}