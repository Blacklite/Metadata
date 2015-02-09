using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blacklite.Framework.Metadata.TagHelpers;

namespace Blacklite.Framework.Metadata.Mvc
{
    public static class BlackliteMetadataMvcServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadataMvc(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Scoped<IModelMetadataProvider, BlackliteMvcModelMetadataProvider>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, HiddenInputPropertyMetadatumResolver>();
            
            // Tag Helper services
            yield return describe.Transient<IConfigureOptions<ControlTagHelperOptions>, ControlTagHelperOptionsSetup>();
            yield return describe.Transient<IControlGenerator, DefaultControlGenerator>();
            //yield return describe.Transient<IConfigureOptions<ControlTagHelperOptions>, BootstrapControlTagHelperOptionsSetup>();
        }
    }
}
