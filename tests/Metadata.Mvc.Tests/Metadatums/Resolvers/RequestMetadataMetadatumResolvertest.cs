﻿using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using System;
using Xunit;
using Moq;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Mvc;
using Blacklite.Framework.Metadata.Mvc.Metadatums.Resolvers;
using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace Metadata.Mvc.Tests.Metadatums.Resolvers
{
    class Visible : IMetadatum { }

    public class RequestMetadataMetadatumResolverTest
    {
        [Fact]
        public void ResolverTypeDoesNotResolveUnlessStoreHasAValue()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataTypeMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<ITypeMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            Assert.False(resolver.CanResolve<Visible>(context));

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve<Visible>(context));
            Assert.Same(visible, resolver.Resolve<Visible>(context));
        }

        [Fact]
        public void ResolverTypePriorityIs1000()
        {
            var store = new RequestMetadataContainer();

            var resolver = new RequestMetadataTypeMetadatumResolver();

            Assert.Equal(int.MaxValue, resolver.Priority);
        }

        [Fact]
        public void ResolverTypeAttemptingToGetAnInvalidValueThrows()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataTypeMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<ITypeMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            Assert.Throws(typeof(IndexOutOfRangeException), () => resolver.Resolve<Visible>(context));
        }

        [Fact]
        public void ResolverTypeSetsItselfAsPartOfTheHttpContext()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataTypeMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<ITypeMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve<Visible>(context));
            Assert.Same(visible, resolver.Resolve<Visible>(context));
            Assert.Same(store, httpContext.Items[typeof(IRequestMetadataContainer)]);
        }

        [Fact]
        public void ResolverPropertyDoesNotResolveUnlessStoreHasAValue()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<IPropertyMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC@Property:DEF");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataPropertyMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<IPropertyMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            Assert.False(resolver.CanResolve<Visible>(context));

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve<Visible>(context));
            Assert.Same(visible, resolver.Resolve<Visible>(context));
        }

        [Fact]
        public void ResolverPropertyPriorityIs1000()
        {
            var store = new RequestMetadataContainer();

            var resolver = new RequestMetadataPropertyMetadatumResolver();

            Assert.Equal(int.MaxValue, resolver.Priority);
        }

        [Fact]
        public void ResolverPropertyAttemptingToGetAnInvalidValueThrows()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<IPropertyMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC@Property:DEF");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataPropertyMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<IPropertyMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            Assert.Throws(typeof(IndexOutOfRangeException), () => resolver.Resolve<Visible>(context));
        }

        [Fact]
        public void ResolverPropertySetsItselfAsPartOfTheHttpContext()
        {
            var store = new RequestMetadataContainer();

            var typeMetadataMock = new Mock<IPropertyMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC@Property:DEF");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new RequestMetadataPropertyMetadatumResolver();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IRequestMetadataContainer))).Returns(store);
            var serviceProvider = serviceProviderMock.Object;

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());
            httpContextMock.SetupGet(x => x.RequestServices).Returns(serviceProvider);
            var httpContext = httpContextMock.Object;

            var contextMock = new Mock<IPropertyMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.HttpContext).Returns(httpContext);

            var context = contextMock.Object;

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve<Visible>(context));
            Assert.Same(visible, resolver.Resolve<Visible>(context));
            Assert.Same(store, httpContext.Items[typeof(IRequestMetadataContainer)]);
        }
    }
}
