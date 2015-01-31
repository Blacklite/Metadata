using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class GroupAttribute : Attribute
    {
        public GroupAttribute(params string[] groups)
        {
            Groups = groups;
        }

        public string[] Groups { get; }
    }
}