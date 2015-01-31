using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Mvc
{
    public static class BlackliteMetadataMvcServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadataMvc(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Scoped<IModelMetadataProvider, BlackliteMvcModelMetadataProvider>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, HiddenInputPropertyMetadatumResolver>();
        }
    }
}
