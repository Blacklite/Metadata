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
    public static class BlackliteMetadataMvcCollectionExtensions
    {
        public static IServiceCollection AddMetadataMvc(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.AddMetadataModeling()
                    .TryAddImplementation(BlackliteMetadataMvcServices.GetMetadataMvc(configuration));
            return services;
        }
    }
}
