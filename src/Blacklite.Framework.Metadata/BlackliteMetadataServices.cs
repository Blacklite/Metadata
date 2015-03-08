using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public static class BlackliteMetadataServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadata(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Singleton<IApplicationMetadataProvider, ApplicationMetadataProvider>();
            yield return describe.Singleton<IPropertyMetadataProvider, PropertyMetadataProvider>();
            yield return describe.Scoped<IMetadataProvider, MetadataProvider>();
            yield return describe.Scoped(typeof(ITypeMetadata<>), typeof(TypeMetadata<>));
            yield return describe.Singleton<ITypeMetadataFactory, TypeMetadataFactory>();
            yield return describe.Singleton<IMetadatumResolverProvider, MetadatumResolverProvider>();
        }

        public static IEnumerable<IServiceDescriptor> GetPropertyDescriptors(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Singleton<IPropertyDescriptor, ReflectionPropertyDescriptor>();
            yield return describe.Transient<IMetadatumResolverProviderCollector, MetadatumResolverProviderCollector>();
            yield return describe.Transient<IMetadatumResolverProviderCollector, ApplicationMetadatumResolverProviderCollector>();
        }

        public static IEnumerable<IServiceDescriptor> GetScopedMetadata(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);

            yield return describe.Scoped<IScopedMetadataContainer, ScopedMetadataContainer>();
            yield return describe.Singleton<ITypeMetadatumResolver, ScopedMetadataTypeMetadatumResolver>();
            yield return describe.Singleton<IPropertyMetadatumResolver, ScopedMetadataPropertyMetadatumResolver>();
        }
    }
}
