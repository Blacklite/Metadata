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
    public static class BlackliteMetadataStorageServices
    {
        public static IEnumerable<IServiceDescriptor> GetMetadataStorage(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);
            yield return describe.Singleton<IApplicationTypeMetadatumResolver, MetadataStorageTypeMetadatumResolver>();
            yield return describe.Singleton<IApplicationPropertyMetadatumResolver, MetadataStoragePropertyMetadatumResolver>();
        }

        public static IEnumerable<IServiceDescriptor> GetInMemoryMetadataStore(IConfiguration configuration = null)
        {
            var describe = new ServiceDescriber(configuration);
            yield return describe.Singleton<IMetadataStorageContainer, InMemoryMetadataStorageContainer>();
        }
    }
}
