using Microsoft.AspNet.Mvc.ModelBinding;
using System;

namespace Blacklite.Framework.Metadata.Mvc
{
    public static class BlackliteMvcModelMetadataExtensions
    {
        public static IMetadata AsMetadata(this ModelMetadata metadata)
        {
            var item = metadata as BlackliteMvcModelMetadata;
            if (item == null)
                throw new NullReferenceException($"ModelMetadata is not the correct type of metadata.");

            return item.Metadata;
        }
    }
}