using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Storage;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataStorageCollectionExtensions
    {
        public static IServiceCollection AddInMemoryMetadataStore(
            [NotNull] this IServiceCollection services,
            )
        {
            ConfigureDefaultServices(services, configuration);
            services.TryAdd(BlackliteMetadataStorageServices.GetInMemoryMetadataStore(configuration));
            return services;
        }

        public static IServiceCollection AddMetadataStorage(
            [NotNull] this IServiceCollection services,
            )
        {
            services.TryAdd(BlackliteMetadataStorageServices.GetMetadataStorage(configuration));
            return services;
        }

        private static void ConfigureDefaultServices([NotNull] IServiceCollection services, IConfiguration configuration)
        {
            services.AddMetadataStorage(configuration);
        }
    }
}
