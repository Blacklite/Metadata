using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Storage;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MetadataStorageCollectionExtensions
    {
        public static IServiceCollection AddInMemoryMetadataStore(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            ConfigureDefaultServices(services, configuration);
            services.TryAdd(MetadataStorageServices.GetInMemoryMetadataStore(configuration));
            return services;
        }

        public static IServiceCollection AddMetadataStorage(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.TryAdd(MetadataStorageServices.GetMetadataStorage(configuration));
            return services;
        }

        private static void ConfigureDefaultServices([NotNull] IServiceCollection services, IConfiguration configuration)
        {
            services.AddMetadataStorage(configuration);
        }
    }
}
