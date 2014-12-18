using System;
using Moq;
using Xunit;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Metadatums;
using System.Linq;

namespace Metadata.Tests.Metadatums.Resolvers
{
    public class MetadatumResolverProviderTests
    {
        class Pretend : IMetadatum { }

        [Fact]
        public void ProperlyOrganizesTypeResolvers()
        {
            var globalResolver1Mock = new Mock<ITypeMetadatumResolver>();
            globalResolver1Mock.SetupGet(x => x.Priority).Returns(-100);
            globalResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver2Mock = new Mock<ITypeMetadatumResolver>();
            globalResolver2Mock.SetupGet(x => x.Priority).Returns(100);
            globalResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver3Mock = new Mock<ITypeMetadatumResolver>();
            globalResolver3Mock.SetupGet(x => x.Priority).Returns(1);
            globalResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);


            var visibleResolver1Mock = new Mock<ITypeMetadatumResolver>();
            visibleResolver1Mock.SetupGet(x => x.Priority).Returns(-99);
            visibleResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver2Mock = new Mock<ITypeMetadatumResolver>();
            visibleResolver2Mock.SetupGet(x => x.Priority).Returns(99);
            visibleResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver3Mock = new Mock<ITypeMetadatumResolver>();
            visibleResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            visibleResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));


            var pretendResolver1Mock = new Mock<ITypeMetadatumResolver>();
            pretendResolver1Mock.SetupGet(x => x.Priority).Returns(99);
            pretendResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver2Mock = new Mock<ITypeMetadatumResolver>();
            pretendResolver2Mock.SetupGet(x => x.Priority).Returns(-99);
            pretendResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver3Mock = new Mock<ITypeMetadatumResolver>();
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
                    globalResolver1, globalResolver2, globalResolver3,
                    visibleResolver1, visibleResolver2, visibleResolver3,
                    pretendResolver1, pretendResolver2, pretendResolver3,
                },
                Enumerable.Empty<IPropertyMetadatumResolver>()
                );

            var resolvers = provider.TypeResolvers;

            var visibleResolvers = resolvers[typeof(Visible)];
            var pretendResolvers = resolvers[typeof(Pretend)];

            Assert.Equal(6, visibleResolvers.Count());
            Assert.Same(globalResolver2, visibleResolvers.First());
            Assert.Same(visibleResolver2, visibleResolvers.Skip(1).First());
            Assert.Same(globalResolver3, visibleResolvers.Skip(2).First());
            Assert.Same(visibleResolver3, visibleResolvers.Skip(3).First());
            Assert.Same(visibleResolver1, visibleResolvers.Skip(4).First());
            Assert.Same(globalResolver1, visibleResolvers.Skip(5).First());

            Assert.Equal(6, pretendResolvers.Count());
            Assert.Same(globalResolver2, pretendResolvers.First());
            Assert.Same(pretendResolver1, pretendResolvers.Skip(1).First());
            Assert.Same(globalResolver3, pretendResolvers.Skip(2).First());
            Assert.Same(pretendResolver3, pretendResolvers.Skip(3).First());
            Assert.Same(pretendResolver2, pretendResolvers.Skip(4).First());
            Assert.Same(globalResolver1, pretendResolvers.Skip(5).First());
        }

        [Fact]
        public void ProperlyOrganizesPropertyResolvers()
        {
            var globalResolver1Mock = new Mock<IPropertyMetadatumResolver>();
            globalResolver1Mock.SetupGet(x => x.Priority).Returns(-100);
            globalResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver2Mock = new Mock<IPropertyMetadatumResolver>();
            globalResolver2Mock.SetupGet(x => x.Priority).Returns(100);
            globalResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);

            var globalResolver3Mock = new Mock<IPropertyMetadatumResolver>();
            globalResolver3Mock.SetupGet(x => x.Priority).Returns(1);
            globalResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(() => null);


            var visibleResolver1Mock = new Mock<IPropertyMetadatumResolver>();
            visibleResolver1Mock.SetupGet(x => x.Priority).Returns(-99);
            visibleResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver2Mock = new Mock<IPropertyMetadatumResolver>();
            visibleResolver2Mock.SetupGet(x => x.Priority).Returns(99);
            visibleResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));

            var visibleResolver3Mock = new Mock<IPropertyMetadatumResolver>();
            visibleResolver3Mock.SetupGet(x => x.Priority).Returns(0);
            visibleResolver3Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Visible));


            var pretendResolver1Mock = new Mock<IPropertyMetadatumResolver>();
            pretendResolver1Mock.SetupGet(x => x.Priority).Returns(99);
            pretendResolver1Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver2Mock = new Mock<IPropertyMetadatumResolver>();
            pretendResolver2Mock.SetupGet(x => x.Priority).Returns(-99);
            pretendResolver2Mock.Setup(x => x.GetMetadatumType()).Returns(typeof(Pretend));

            var pretendResolver3Mock = new Mock<IPropertyMetadatumResolver>();
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
                Enumerable.Empty<ITypeMetadatumResolver>(),
                new[] {
                    globalResolver1, globalResolver2, globalResolver3,
                    visibleResolver1, visibleResolver2, visibleResolver3,
                    pretendResolver1, pretendResolver2, pretendResolver3,
                });

            var resolvers = provider.PropertyResolvers;

            var visibleResolvers = resolvers[typeof(Visible)];
            var pretendResolvers = resolvers[typeof(Pretend)];

            Assert.Equal(6, visibleResolvers.Count());
            Assert.Same(globalResolver2, visibleResolvers.First());
            Assert.Same(visibleResolver2, visibleResolvers.Skip(1).First());
            Assert.Same(globalResolver3, visibleResolvers.Skip(2).First());
            Assert.Same(visibleResolver3, visibleResolvers.Skip(3).First());
            Assert.Same(visibleResolver1, visibleResolvers.Skip(4).First());
            Assert.Same(globalResolver1, visibleResolvers.Skip(5).First());

            Assert.Equal(6, pretendResolvers.Count());
            Assert.Same(globalResolver2, pretendResolvers.First());
            Assert.Same(pretendResolver1, pretendResolvers.Skip(1).First());
            Assert.Same(globalResolver3, pretendResolvers.Skip(2).First());
            Assert.Same(pretendResolver3, pretendResolvers.Skip(3).First());
            Assert.Same(pretendResolver2, pretendResolvers.Skip(4).First());
            Assert.Same(globalResolver1, pretendResolvers.Skip(5).First());
        }
    }
}
