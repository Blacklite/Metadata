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
        public void ResolverDoesNotResolveUnlessStoreHasAValue()
        {
            var store = new InMemoryMetadataStore();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStorageMetadatumResolver(store);

            var contextMock = new Mock<ITypeMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            var context = contextMock.Object;

            Assert.False(resolver.CanResolve<Visible>(context));

            var visible = new Visible();
            store.Save(typeMetadata, visible);

            Assert.True(resolver.CanResolve<Visible>(context));
            Assert.Same(visible, resolver.Resolve<Visible>(context));
        }

        [Fact]
        public void ResolverPriorityIs1000()
        {
            var store = new InMemoryMetadataStore();

            var resolver = new MetadataStorageMetadatumResolver(store);

            Assert.Equal(1000, resolver.Priority);
        }

        [Fact]
        public void ResolverAttemptingToGetAnInvalidValueThrows()
        {
            var store = new InMemoryMetadataStore();

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Key).Returns("Type:ABC");
            var typeMetadata = typeMetadataMock.Object;

            var resolver = new MetadataStorageMetadatumResolver(store);

            Assert.Equal(1000, resolver.Priority);

            var contextMock = new Mock<ITypeMetadatumResolutionContext>();
            contextMock.SetupGet(x => x.Metadata).Returns(typeMetadata);
            var context = contextMock.Object;

            Assert.Throws(typeof(IndexOutOfRangeException), () => resolver.Resolve<Visible>(context));
        }
    }
}
