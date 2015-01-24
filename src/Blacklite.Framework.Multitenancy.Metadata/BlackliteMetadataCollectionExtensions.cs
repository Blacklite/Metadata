using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Multitenancy.Metadata;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMultitenancyMetadataCollectionExtensions
    {
        public static IServiceCollection AddMetadata(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.TryAdd(BlackliteMetadataServices.GetMetadata(configuration));
            services.Add(BlackliteMultitenancyMetadataServices.GetMultitenancyMetadata(configuration));
            return services;
        }
    }
}
