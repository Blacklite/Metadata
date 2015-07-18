using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataCollectionExtensions
    {
        public static IServiceCollection AddMetadata([NotNull] this IServiceCollection services)
        {
            services.TryAdd(BlackliteMetadataServices.GetMetadata());
            services.TryAddImplementation(BlackliteMetadataServices.GetPropertyDescriptors());
            return services;
        }

        public static IServiceCollection AddScopedMetadata([NotNull] this IServiceCollection services)
        {
            services.AddMetadata()
                    .TryAddImplementation(BlackliteMetadataServices.GetScopedMetadata());
            return services;
        }
    }
}
