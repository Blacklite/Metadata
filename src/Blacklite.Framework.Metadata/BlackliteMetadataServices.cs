using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public static class BlackliteMetadataServices
    {
        public static IEnumerable<ServiceDescriptor> GetMetadata()
        {
            yield return ServiceDescriptor.Singleton<IApplicationMetadataProvider, ApplicationMetadataProvider>();
            yield return ServiceDescriptor.Singleton<IPropertyMetadataProvider, PropertyMetadataProvider>();
            yield return ServiceDescriptor.Scoped<IMetadataProvider, MetadataProvider>();
            yield return ServiceDescriptor.Scoped(typeof(ITypeMetadata<>), typeof(TypeMetadata<>));
            yield return ServiceDescriptor.Singleton<ITypeMetadataFactory, TypeMetadataFactory>();
            yield return ServiceDescriptor.Singleton<IMetadatumResolverProvider, MetadatumResolverProvider>();
        }

        public static IEnumerable<ServiceDescriptor> GetPropertyDescriptors()
        {
            yield return ServiceDescriptor.Singleton<IPropertyDescriptor, ReflectionPropertyDescriptor>();
            yield return ServiceDescriptor.Transient<IMetadatumResolverProviderCollector, MetadatumResolverProviderCollector>();
            yield return ServiceDescriptor.Transient<IMetadatumResolverProviderCollector, ApplicationMetadatumResolverProviderCollector>();
        }

        public static IEnumerable<ServiceDescriptor> GetScopedMetadata()
        {
            yield return ServiceDescriptor.Scoped<IScopedMetadataContainer, ScopedMetadataContainer>();
            yield return ServiceDescriptor.Singleton<ITypeMetadatumResolver, ScopedMetadataTypeMetadatumResolver>();
            yield return ServiceDescriptor.Singleton<IPropertyMetadatumResolver, ScopedMetadataPropertyMetadatumResolver>();
        }
    }
}
