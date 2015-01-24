using System;
using Moq;
using Xunit;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Metadatums;
using System.Linq;
using Blacklite.Framework.Metadata;

namespace Metadata.Tests.Metadatums.Resolvers
{
    public class MetadatumResolverProviderTests
    {
        class Pretend : IMetadatum { }

        public abstract class TypeMetadatumResolver : ITypeMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract Type GetMetadatumType();

            public abstract bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context);

            public abstract IMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context);
        }

        [Fact]
        public void ProperlyOrganizesTypeResolvers()
        {
            var globalResolver1Mock = new Mock<TypeMetadatumResolver>();
            globalResolver1Mock.SetupGet(x => x.Priority).Returns(-100);
            globalResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver2Mock = new Mock<TypeMetadatumResolver>();
            globalResolver2Mock.SetupGet(x => x.Priority).Returns(100);
            globalResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver3Mock = new Mock<TypeMetadatumResolver>();
            globalResolver3Mock.SetupGet(x => x.Priority).Returns(1);
            globalResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);


            var visibleResolver1Mock = new Mock<TypeMetadatumResolver>();
            visibleResolver1Mock.SetupGet(x => x.Priority).Returns(-99);
            visibleResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver2Mock = new Mock<TypeMetadatumResolver>();
            visibleResolver2Mock.SetupGet(x => x.Priority).Returns(99);
            visibleResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver3Mock = new Mock<TypeMetadatumResolver>();
            visibleResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            visibleResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));


            var pretendResolver1Mock = new Mock<TypeMetadatumResolver>();
            pretendResolver1Mock.SetupGet(x => x.Priority).Returns(99);
            pretendResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver2Mock = new Mock<TypeMetadatumResolver>();
            pretendResolver2Mock.SetupGet(x => x.Priority).Returns(-99);
            pretendResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver3Mock = new Mock<TypeMetadatumResolver>();
            pretendResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            pretendResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));


            var globalResolver1 = globalResolver1Mock.Object;
            var globalResolver2 = globalResolver2Mock.Object;
            var globalResolver3 = globalResolver3Mock.Object;

            var visibleResolver1 = visibleResolver1Mock.Object;
            var visibleResolver2 = visibleResolver2Mock.Object;
            var visibleResolver3 = visibleResolver3Mock.Object;

            var pretendResolver1 = pretendResolver1Mock.Object;
            var pretendResolver2 = pretendResolver2Mock.Object;
            var pretendResolver3 = pretendResolver3Mock.Object;

            var provider = new MetadatumResolverProvider(
                new[] {
                    new MetadatumResolverProviderCollector(
                    new[] {
                        globalResolver1, globalResolver2, globalResolver3,
                        visibleResolver1, visibleResolver2, visibleResolver3,
                        pretendResolver1, pretendResolver2, pretendResolver3,
                    },
                    Enumerable.Empty<IPropertyMetadatumResolver>()
                )});

            var visibleResolvers = provider.GetTypeResolvers("Default", typeof(Visible)).Cast<MetadatumResolverDescriptor<ITypeMetadatumResolver, ITypeMetadata>>();
            var pretendResolvers = provider.GetTypeResolvers("Default", typeof(Pretend)).Cast<MetadatumResolverDescriptor<ITypeMetadatumResolver, ITypeMetadata>>();

            Assert.Equal(6, visibleResolvers.Count());
            Assert.Same(globalResolver2, visibleResolvers.First().Resolver);
            Assert.Same(visibleResolver2, visibleResolvers.Skip(1).First().Resolver);
            Assert.Same(globalResolver3, visibleResolvers.Skip(2).First().Resolver);
            Assert.Same(visibleResolver3, visibleResolvers.Skip(3).First().Resolver);
            Assert.Same(visibleResolver1, visibleResolvers.Skip(4).First().Resolver);
            Assert.Same(globalResolver1, visibleResolvers.Skip(5).First().Resolver);

            Assert.Equal(6, pretendResolvers.Count());
            Assert.Same(globalResolver2, pretendResolvers.First().Resolver);
            Assert.Same(pretendResolver1, pretendResolvers.Skip(1).First().Resolver);
            Assert.Same(globalResolver3, pretendResolvers.Skip(2).First().Resolver);
            Assert.Same(pretendResolver3, pretendResolvers.Skip(3).First().Resolver);
            Assert.Same(pretendResolver2, pretendResolvers.Skip(4).First().Resolver);
            Assert.Same(globalResolver1, pretendResolvers.Skip(5).First().Resolver);
        }

        public abstract class PropertyMetadatumResolver : IPropertyMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract Type GetMetadatumType();

            public abstract bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context);

            public abstract IMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context);
        }

        [Fact]
        public void ProperlyOrganizesPropertyResolvers()
        {
            var globalResolver1Mock = new Mock<PropertyMetadatumResolver>();
            globalResolver1Mock.SetupGet(x => x.Priority).Returns(-100);
            globalResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver2Mock = new Mock<PropertyMetadatumResolver>();
            globalResolver2Mock.SetupGet(x => x.Priority).Returns(100);
            globalResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver3Mock = new Mock<PropertyMetadatumResolver>();
            globalResolver3Mock.SetupGet(x => x.Priority).Returns(1);
            globalResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);


            var visibleResolver1Mock = new Mock<PropertyMetadatumResolver>();
            visibleResolver1Mock.SetupGet(x => x.Priority).Returns(-99);
            visibleResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver2Mock = new Mock<PropertyMetadatumResolver>();
            visibleResolver2Mock.SetupGet(x => x.Priority).Returns(99);
            visibleResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver3Mock = new Mock<PropertyMetadatumResolver>();
            visibleResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            visibleResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));


            var pretendResolver1Mock = new Mock<PropertyMetadatumResolver>();
            pretendResolver1Mock.SetupGet(x => x.Priority).Returns(99);
            pretendResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver2Mock = new Mock<PropertyMetadatumResolver>();
            pretendResolver2Mock.SetupGet(x => x.Priority).Returns(-99);
            pretendResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver3Mock = new Mock<PropertyMetadatumResolver>();
            pretendResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            pretendResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));


            var globalResolver1 = globalResolver1Mock.Object;
            var globalResolver2 = globalResolver2Mock.Object;
            var globalResolver3 = globalResolver3Mock.Object;

            var visibleResolver1 = visibleResolver1Mock.Object;
            var visibleResolver2 = visibleResolver2Mock.Object;
            var visibleResolver3 = visibleResolver3Mock.Object;

            var pretendResolver1 = pretendResolver1Mock.Object;
            var pretendResolver2 = pretendResolver2Mock.Object;
            var pretendResolver3 = pretendResolver3Mock.Object;

            var provider = new MetadatumResolverProvider(new[] {
                    new MetadatumResolverProviderCollector(
                        Enumerable.Empty<ITypeMetadatumResolver>(),
                        new[] {
                            globalResolver1, globalResolver2, globalResolver3,
                            visibleResolver1, visibleResolver2, visibleResolver3,
                            pretendResolver1, pretendResolver2, pretendResolver3,
                        }
                    )});


            var visibleResolvers = provider.GetPropertyResolvers("Default", typeof(Visible)).Cast<MetadatumResolverDescriptor<IPropertyMetadatumResolver, IPropertyMetadata>>();
            var pretendResolvers = provider.GetPropertyResolvers("Default", typeof(Pretend)).Cast<MetadatumResolverDescriptor<IPropertyMetadatumResolver, IPropertyMetadata>>();

            Assert.Equal(6, visibleResolvers.Count());
            Assert.Same(globalResolver2, visibleResolvers.First().Resolver);
            Assert.Same(visibleResolver2, visibleResolvers.Skip(1).First().Resolver);
            Assert.Same(globalResolver3, visibleResolvers.Skip(2).First().Resolver);
            Assert.Same(visibleResolver3, visibleResolvers.Skip(3).First().Resolver);
            Assert.Same(visibleResolver1, visibleResolvers.Skip(4).First().Resolver);
            Assert.Same(globalResolver1, visibleResolvers.Skip(5).First().Resolver);

            Assert.Equal(6, pretendResolvers.Count());
            Assert.Same(globalResolver2, pretendResolvers.First().Resolver);
            Assert.Same(pretendResolver1, pretendResolvers.Skip(1).First().Resolver);
            Assert.Same(globalResolver3, pretendResolvers.Skip(2).First().Resolver);
            Assert.Same(pretendResolver3, pretendResolvers.Skip(3).First().Resolver);
            Assert.Same(pretendResolver2, pretendResolvers.Skip(4).First().Resolver);
            Assert.Same(globalResolver1, pretendResolvers.Skip(5).First().Resolver);
        }
    }
}
