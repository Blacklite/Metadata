using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace Blacklite.Framework.Metadata.Mvc
{
    class BlackliteMvcModelMetadataProvider : DefaultModelMetadataProvider
    {
        private readonly IMetadataProvider _metadataProvider;
        private readonly IScopedMetadataContainer _metadataContainer;

        public BlackliteMvcModelMetadataProvider(IMetadataProvider metadataProvider, IScopedMetadataContainer metadataContainer, ICompositeMetadataDetailsProvider detailsProvider)
            : base(detailsProvider)
        {
            _metadataProvider = metadataProvider;
            _metadataContainer = metadataContainer;
        }

        protected override ModelMetadata CreateModelMetadata(DefaultMetadataDetails entry)
        {
            return base.CreateModelMetadata(entry);
        }
    }
}
