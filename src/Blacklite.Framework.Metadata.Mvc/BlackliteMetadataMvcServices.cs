﻿using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers;
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
        public static IEnumerable<IServiceDescriptor> GetPerRequestMetadata(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Scoped<IRequestMetadataContainer, RequestMetadataContainer>();
            yield return describe.Singleton<ITypeMetadatumResolver, RequestMetadataTypeMetadatumResolver>();
            yield return describe.Singleton<IPropertyMetadatumResolver, RequestMetadataPropertyMetadatumResolver>();
        }
    }
}
