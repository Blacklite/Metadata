using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Microsoft.Framework.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Blacklite.Framework.Metadata.Modeling;
using Blacklite.Framework.Metadata.Properties;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers
{
    class ReadOnlyPropertyMetadatumResolver : SimplePropertyMetadatumResolver<ReadOnly>
    {
        public override ReadOnly Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
        {
            var editableAttribute = context.Metadata.PropertyInfo
                .CustomAttributes.OfType<EditableAttribute>()
                .SingleOrDefault();

            if (editableAttribute != null)
                return new ReadOnly(!editableAttribute.AllowEdit);

            var readOnlyAttribute = context.Metadata.PropertyInfo
                .CustomAttributes.OfType<ReadOnlyAttribute>()
                .SingleOrDefault();

            if (readOnlyAttribute != null)
                return new ReadOnly(readOnlyAttribute.ReadOnly);

            return new ReadOnly(false);
        }
    }
}
