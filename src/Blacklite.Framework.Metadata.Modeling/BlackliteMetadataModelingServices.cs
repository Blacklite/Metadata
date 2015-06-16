using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Modeling
{
    public static class BlackliteMetadataMvcServices
    {
        public static IEnumerable<ServiceDescriptor> GetMetadataModeling()
        {
            yield return describe.Transient<IApplicationTypeMetadatumResolver, DisplayNameTypeMetadatumResolver>();
            yield return describe.Transient<IApplicationTypeMetadatumResolver, DescriptionTypeMetadatumResolver>();
            yield return describe.Transient<IApplicationTypeMetadatumResolver, ShortNameTypeMetadatumResolver>();
            yield return describe.Transient<IApplicationTypeMetadatumResolver, OrderTypeMetadatumResolver>();
            yield return describe.Transient<IApplicationTypeMetadatumResolver, GroupTypeMetadatumResolver>();

            yield return describe.Transient<IApplicationPropertyMetadatumResolver, DisplayNamePropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, DescriptionPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, ShortNamePropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, OrderPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, GroupPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, DisplayFormatPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, EditFormatPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, ShowForDisplayPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, ShowForEditPropertyMetadatumResolver>();

            yield return describe.Transient<IApplicationPropertyMetadatumResolver, CompareMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, InfoTypePropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, LengthPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, ReadOnlyPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, RegularExpressionPropertyMetadatumResolver>();
            yield return describe.Transient<IApplicationPropertyMetadatumResolver, RequiredPropertyMetadatumResolver>();
        }
    }
}
