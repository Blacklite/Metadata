using System;

namespace Blacklite.Framework.Metadata.Modeling
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EditFormatAttribute : Attribute
    {
        public EditFormatAttribute() { }

        public bool ConvertEmptyStringToNull { get; set; }

        public string DataFormatString { get; set; }

        public bool HtmlEncode { get; set; }

        public string NullDisplayText { get; set; }
    }
}