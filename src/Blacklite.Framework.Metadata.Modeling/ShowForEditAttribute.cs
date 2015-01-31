using System;

namespace Blacklite.Framework.Metadata.Modeling
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowForEditAttribute : Attribute
    {
        public ShowForEditAttribute(bool visible)
        {
            Visible = visible;
        }

        public bool Visible { get; }
    }
}