using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using System.ComponentModel.DataAnnotations;
using Blacklite.Framework;
using Blacklite.Framework.Metadata;
using System.Linq;
using System.Reflection;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc;

namespace Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers
{

    class HiddenInputPropertyMetadatumResolver : SimplePropertyMetadatumResolver<HiddenInput>
    {
        public override HiddenInput Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var description = context.Metadata.Attributes
                .OfType<HiddenInputAttribute>()
                .SingleOrDefault()?.DisplayValue ?? false;

            return new HiddenInput(description);
        }
    }
}