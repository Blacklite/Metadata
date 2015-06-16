using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Lifetimes;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MetadataLifetimesCollectionExtensions
    {
        public static IServiceCollection AddMetadataLifetimes(
            [NotNull] this IServiceCollection services,
            )
        {
            ConfigureDefaultServices(services, configuration);
            services.Add(MetadataLifetimesServices.GetMetadataLifetimes(configuration));
            return services;
        }

        private static void ConfigureDefaultServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMetadata(configuration);
        }
    }
}
