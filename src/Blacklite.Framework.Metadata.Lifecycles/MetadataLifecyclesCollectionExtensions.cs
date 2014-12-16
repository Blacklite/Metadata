using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Lifecycles;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MetadataLifecyclesCollectionExtensions
    {
        public static IServiceCollection AddMetadataLifecycles(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            ConfigureDefaultServices(services, configuration);
            services.Add(MetadataLifecyclesServices.GetMetadataLifecycles(configuration));
            return services;
        }

        private static void ConfigureDefaultServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMetadata(configuration);
        }
    }
}
