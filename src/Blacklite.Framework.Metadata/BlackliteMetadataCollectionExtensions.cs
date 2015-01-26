using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataCollectionExtensions
    {
        public static IServiceCollection AddMetadata(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.TryAdd(BlackliteMetadataServices.GetMetadata(configuration));
            services.TryAddImplementation(BlackliteMetadataServices.GetPropertyDescriptors(configuration));
            return services;
        }
    }
}
