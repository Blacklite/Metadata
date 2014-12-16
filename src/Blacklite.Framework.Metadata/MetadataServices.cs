﻿using Blacklite.Framework.Metadata.MetadataProperties;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public static class MetadataServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadata(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Scoped<IMetadataProvider, MetadataProvider>();
            yield return describe.Singleton<IPropertyMetadataProvider, PropertyMetadataProvider>();
            yield return describe.Scoped(typeof(ITypeMetadata<>), typeof(TypeMetadata<>));
            yield return describe.Singleton<IPropertyDescriptor, ReflectionPropertyDescriptor>();
        }
    }
}