using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Storage;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataStorageCollectionExtensions
    {
        public static IServiceCollection AddInMemoryMetadataStore([NotNull] this IServiceCollection services)
        {
            ConfigureDefaultServices(services);
            services.TryAdd(BlackliteMetadataStorageServices.GetInMemoryMetadataStore());
            return services;
        }

        public static IServiceCollection AddMetadataStorage([NotNull] this IServiceCollection services)
        {
            services.TryAdd(BlackliteMetadataStorageServices.GetMetadataStorage());
            return services;
        }

        private static void ConfigureDefaultServices([NotNull] IServiceCollection services)
        {
            services.AddMetadataStorage();
        }
    }
}
