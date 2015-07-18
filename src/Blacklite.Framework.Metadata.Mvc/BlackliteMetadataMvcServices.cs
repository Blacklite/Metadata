using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers;
using Microsoft.AspNet.Mvc.ModelBinding;
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
        public static IEnumerable<ServiceDescriptor> GetMetadataMvc()
        {
            yield return ServiceDescriptor.Scoped<IModelMetadataProvider, BlackliteMvcModelMetadataProvider>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, HiddenInputPropertyMetadatumResolver>();

            // Tag Helper services
            yield return ServiceDescriptor.Transient<IConfigureOptions<ControlTagHelperOptions>, ControlTagHelperOptionsSetup>();
            yield return ServiceDescriptor.Transient<IControlGenerator, DefaultControlGenerator>();
            //yield return ServiceDescriptor.Transient<IConfigureOptions<ControlTagHelperOptions>, BootstrapControlTagHelperOptionsSetup>();
        }
    }
}
