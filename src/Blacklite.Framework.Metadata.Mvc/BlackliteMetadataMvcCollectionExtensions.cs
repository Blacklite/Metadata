using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataCollectionExtensions
    {
        public static IServiceCollection AddPerRequestMetadata(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.AddMetadata()
                    .TryAddImplementation(BlackliteMetadataMvcServices.GetPerRequestMetadata(configuration));
            return services;
        }
    }
}
