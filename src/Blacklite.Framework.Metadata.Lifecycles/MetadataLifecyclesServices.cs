using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Lifecycles
{
    public static class MetadataLifecyclesServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadataLifecycles(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Singleton<IMetadataPropertyProvider, LifecycleMetadataPropertyProvider>();
        }
    }
}
