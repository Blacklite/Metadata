using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata.Lifetimes;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class MetadataLifetimesCollectionExtensions
    {
        public static IServiceCollection AddMetadataLifetimes(            [NotNull] this IServiceCollection services            )
        {
            ConfigureDefaultServices(services);
            services.Add(MetadataLifetimesServices.GetMetadataLifetimes());
            return services;
        }

        private static void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddMetadata();
        }
    }
}
