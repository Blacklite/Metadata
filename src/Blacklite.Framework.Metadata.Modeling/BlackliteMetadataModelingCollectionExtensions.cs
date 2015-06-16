using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Modeling;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataModelingCollectionExtensions
    {
        public static IServiceCollection AddMetadataModeling(
            [NotNull] this IServiceCollection services,
            )
        {
            services.AddMetadata()
                    .TryAddImplementation(BlackliteMetadataMvcServices.GetMetadataModeling(configuration));
            return services;
        }
    }
}
