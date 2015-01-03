using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Storage.Metadatums.Resolvers;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Storage
{
    public static class MetadataStorageServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadataStorage(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);
            yield return describe.Singleton<ITypeMetadatumResolver, MetadataStorageMetadatumResolver>();
        }

        public static IEnumerable<IServiceDescriptor> GetInMemoryMetadataStore(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);
            yield return describe.Singleton<IMetadataStore, InMemoryMetadataStore>();
        }
    }
}
