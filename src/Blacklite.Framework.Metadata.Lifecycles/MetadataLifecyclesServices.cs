using Blacklite.Framework.Metadata.Properties;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Lifetimes
{
    public static class MetadataLifetimesServices
    {
        public static IEnumerable<ServiceDescriptor> GetMetadataLifetimes()
        {
            yield return describe.Singleton<IPropertyMetadataProvider, LifetimeMetadataPropertyProvider>();
        }
    }
}
