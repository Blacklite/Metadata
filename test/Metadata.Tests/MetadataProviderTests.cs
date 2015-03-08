using Blacklite.Framework.Metadata;
using System;
using Moq;
using Xunit;
using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata.Metadatums;
using System.Reflection;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;

namespace Metadata.Tests
{

    class Visible : IMetadatum
    {
        public Visible(bool value)
        {
            Value = value;
        }

        public bool Value { get; }
    }

    public class MetadataProviderTests
    {
        private class Type1 { }

        private class Type2 { }

        [Fact]
        public void ShouldReturnCachedVersionsOfTheMetadata()
        {
            var propertyMetadataProviderMock = new Mock<IPropertyMetadataProvider>();
            var propertyMetadataProvider = propertyMetadataProviderMock.Object;

            var typeMetadata1Mock = new Mock<IApplicationTypeMetadata>();
            var typeMetadata1 = typeMetadata1Mock.Object;

            typeMetadata1Mock.SetupGet(x => x.Name).Returns("Type1");

            var typeMetadata2Mock = new Mock<IApplicationTypeMetadata>();
            var typeMetadata2 = typeMetadata2Mock.Object;

            typeMetadata2Mock.SetupGet(x => x.Name).Returns("Type1");

            var propertyMetadata1Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata1 = propertyMetadata1Mock.Object;

            var propertyMetadata2Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata2 = propertyMetadata2Mock.Object;

            var propertyMetadata3Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata3 = propertyMetadata3Mock.Object;

            var propertyMetadata4Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata4 = propertyMetadata4Mock.Object;

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type1")))
                .Returns(new[] { propertyMetadata1, propertyMetadata2, propertyMetadata3 });

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type2")))
                .Returns(new[] { propertyMetadata1, propertyMetadata3, propertyMetadata4 });

            var provider = new ApplicationMetadataProvider(Mock.Of<IServiceProvider>(), propertyMetadataProvider, new Mock<IMetadatumResolverProvider>().Object);

            var type1TypeMetadata = provider.GetMetadata(typeof(Type1));

            var type1TypeInfoMetadata = provider.GetMetadata(typeof(Type1).GetTypeInfo());

            var type1GenericMetadata = provider.GetMetadata<Type1>();

            Assert.Equal(type1TypeMetadata, type1TypeInfoMetadata);
            Assert.Equal(type1TypeMetadata, type1GenericMetadata);
            Assert.Equal(type1TypeInfoMetadata, type1GenericMetadata);
        }

        [Fact]
        public void ShouldReturnDifferentMetadataPerType()
        {
            var propertyMetadataProviderMock = new Mock<IPropertyMetadataProvider>();
            var propertyMetadataProvider = propertyMetadataProviderMock.Object;

            var typeMetadata1Mock = new Mock<IApplicationTypeMetadata>();
            var typeMetadata1 = typeMetadata1Mock.Object;

            typeMetadata1Mock.SetupGet(x => x.Name).Returns("Type1");

            var typeMetadata2Mock = new Mock<IApplicationTypeMetadata>();
            var typeMetadata2 = typeMetadata2Mock.Object;

            typeMetadata2Mock.SetupGet(x => x.Name).Returns("Type2");

            var propertyMetadata1Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata1 = propertyMetadata1Mock.Object;

            var propertyMetadata2Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata2 = propertyMetadata2Mock.Object;

            var propertyMetadata3Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata3 = propertyMetadata3Mock.Object;

            var propertyMetadata4Mock = new Mock<IPropertyMetadata>();
            var propertyMetadata4 = propertyMetadata4Mock.Object;

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type1")))
                .Returns(new[] { propertyMetadata1, propertyMetadata2, propertyMetadata3 });

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type2")))
                .Returns(new[] { propertyMetadata1, propertyMetadata3, propertyMetadata4 });

            var provider = new ApplicationMetadataProvider(Mock.Of<IServiceProvider>(), propertyMetadataProvider, new Mock<IMetadatumResolverProvider>().Object);

            var type1TypeMetadata = provider.GetMetadata(typeof(Type1));

            Assert.Contains(propertyMetadata1, type1TypeMetadata.Properties);
            Assert.Contains(propertyMetadata2, type1TypeMetadata.Properties);
            Assert.Contains(propertyMetadata3, type1TypeMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata4, type1TypeMetadata.Properties);

            var type1TypeInfoMetadata = provider.GetMetadata(typeof(Type1).GetTypeInfo());

            Assert.Contains(propertyMetadata1, type1TypeInfoMetadata.Properties);
            Assert.Contains(propertyMetadata2, type1TypeInfoMetadata.Properties);
            Assert.Contains(propertyMetadata3, type1TypeInfoMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata4, type1TypeInfoMetadata.Properties);

            var type1GenericMetadata = provider.GetMetadata<Type1>();

            Assert.Contains(propertyMetadata1, type1GenericMetadata.Properties);
            Assert.Contains(propertyMetadata2, type1GenericMetadata.Properties);
            Assert.Contains(propertyMetadata3, type1GenericMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata4, type1GenericMetadata.Properties);

            var type2TypeMetadata = provider.GetMetadata(typeof(Type2));

            Assert.Contains(propertyMetadata1, type2TypeMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata2, type2TypeMetadata.Properties);
            Assert.Contains(propertyMetadata3, type2TypeMetadata.Properties);
            Assert.Contains(propertyMetadata4, type2TypeMetadata.Properties);

            var type2TypeInfoMetadata = provider.GetMetadata(typeof(Type2).GetTypeInfo());

            Assert.Contains(propertyMetadata1, type2TypeInfoMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata2, type2TypeInfoMetadata.Properties);
            Assert.Contains(propertyMetadata3, type2TypeInfoMetadata.Properties);
            Assert.Contains(propertyMetadata4, type2TypeInfoMetadata.Properties);

            var type2GenericMetadata = provider.GetMetadata<Type2>();

            Assert.Contains(propertyMetadata1, type2GenericMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata2, type2GenericMetadata.Properties);
            Assert.Contains(propertyMetadata3, type2GenericMetadata.Properties);
            Assert.Contains(propertyMetadata4, type2GenericMetadata.Properties);

            Assert.NotEqual(type1TypeMetadata, type2TypeMetadata);
            Assert.NotEqual(type1TypeInfoMetadata, type2TypeInfoMetadata);
            Assert.NotEqual(type1GenericMetadata, type2GenericMetadata);
        }
    }
}
