using System;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowForDisplayAttribute : Attribute
    {
        public ShowForDisplayAttribute(bool visible)
        {
            Visible = visible;
        }

        public bool Visible { get; }
    }
}