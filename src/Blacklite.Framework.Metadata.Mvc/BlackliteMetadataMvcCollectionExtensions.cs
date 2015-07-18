using Blacklite;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Mvc;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Framework.DependencyInjection
{
    public static class BlackliteMetadataMvcCollectionExtensions
    {
        public static IServiceCollection AddMetadataMvc(            [NotNull] this IServiceCollection services            )
        {
            services.AddMetadataModeling()
                    .TryAddImplementation(BlackliteMetadataMvcServices.GetMetadataMvc());
            return services;
        }
    }
}
