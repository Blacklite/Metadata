using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Storage;
using Blacklite.Framework.Metadata.Storage.Metadatums.Resolvers;
using System;
using Xunit;
using Moq;
using Blacklite.Framework.Metadata;

namespace Metadata.Storage.Tests.Metadatums.Resolvers
{
    class Visible : IMetadatum { }

    public class MetadataStorageMetadatumResolverTest
    {
        [Fact]
        public void ResolverTypeDoesNotResolveUnlessStoreHasAValue()
        {
            var store = new InMemoryMetadataStorageContainer();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStorageTypeMetadatumResolver(store);

            var contextMock = new Mock<IMetadatumResolutionContext<ITypeMetadata>>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.MetadatumType).Returns(typeof(Visible));
            var context = contextMock.Object;

            Assert.False(resolver.CanResolve(context));

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve(context));
            Assert.Same(visible, resolver.Resolve(context));
        }

        [Fact]
        public void ResolverTypePriorityIs1000()
        {
            var store = new InMemoryMetadataStorageContainer();

            var resolver = new MetadataStorageTypeMetadatumResolver(store);

            Assert.Equal(1000, resolver.Priority);
        }

        [Fact]
        public void ResolverTypeAttemptingToGetAnInvalidValueThrows()
        {
            var store = new InMemoryMetadataStorageContainer();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStorageTypeMetadatumResolver(store);

            var contextMock = new Mock<IMetadatumResolutionContext<ITypeMetadata>>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.MetadatumType).Returns(typeof(Visible));
            var context = contextMock.Object;

            Assert.Throws(typeof(IndexOutOfRangeException), () => resolver.Resolve(context));
        }

        [Fact]
        public void ResolverPropertyDoesNotResolveUnlessStoreHasAValue()
        {
            var store = new InMemoryMetadataStorageContainer();

            var typeMetadataMock = new Mock<IPropertyMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC@Property:DEF");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStoragePropertyMetadatumResolver(store);

            var contextMock = new Mock<IMetadatumResolutionContext<IPropertyMetadata>>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.MetadatumType).Returns(typeof(Visible));
            var context = contextMock.Object;

            Assert.False(resolver.CanResolve(context));

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve(context));
            Assert.Same(visible, resolver.Resolve(context));
        }

        [Fact]
        public void ResolverPropertyPriorityIs1000()
        {
            var store = new InMemoryMetadataStorageContainer();

            var resolver = new MetadataStoragePropertyMetadatumResolver(store);

            Assert.Equal(1000, resolver.Priority);
        }

        [Fact]
        public void ResolverPropertyAttemptingToGetAnInvalidValueThrows()
        {
            var store = new InMemoryMetadataStorageContainer();

            var typeMetadataMock = new Mock<IPropertyMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC@Property:DEF");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStoragePropertyMetadatumResolver(store);

            var contextMock = new Mock<IMetadatumResolutionContext<IPropertyMetadata>>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            contextMock.SetupGet(x => x.MetadatumType).Returns(typeof(Visible));
            var context = contextMock.Object;

            Assert.Throws(typeof(IndexOutOfRangeException), () => resolver.Resolve(context));
        }
    }
}
