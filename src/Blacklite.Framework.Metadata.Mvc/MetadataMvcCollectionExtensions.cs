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
    public static class MetadataCollectionExtensions
    {
        public static IServiceCollection AddScopedMetadata(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration = null)
        {
            services.TryAdd(MetadataMvcServices.GetMvcMetadata(configuration));
            services.TryAdd(MetadataServices.GetMetadata(configuration));
            return services;
        }
    }
}
