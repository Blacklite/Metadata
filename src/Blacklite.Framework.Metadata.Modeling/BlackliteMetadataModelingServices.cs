using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Modeling.Metadatums.Resolvers;
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
            yield return ServiceDescriptor.Transient<IApplicationTypeMetadatumResolver, DisplayNameTypeMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationTypeMetadatumResolver, DescriptionTypeMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationTypeMetadatumResolver, ShortNameTypeMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationTypeMetadatumResolver, OrderTypeMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationTypeMetadatumResolver, GroupTypeMetadatumResolver>();

            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, DisplayNamePropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, DescriptionPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, ShortNamePropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, OrderPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, GroupPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, DisplayFormatPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, EditFormatPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, ShowForDisplayPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, ShowForEditPropertyMetadatumResolver>();

            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, CompareMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, InfoTypePropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, LengthPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, ReadOnlyPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, RegularExpressionPropertyMetadatumResolver>();
            yield return ServiceDescriptor.Transient<IApplicationPropertyMetadatumResolver, RequiredPropertyMetadatumResolver>();
        }
    }
}
