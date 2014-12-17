using System;
using Xunit;
using Moq;
using Blacklite.Framework.Metadata.MetadataProperties;
using Blacklite.Framework.Metadata.Metadatums;
using System.Reflection;
using Blacklite.Framework.Metadata;
using System.Linq;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;

namespace Metadata.Tests.MetadataProperties
{
    public class PropertyMetadataProviderTests
    {
        [Fact]
        public void ShouldReturnProperties()
        {
            var properties1 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = 0,
                    PropertyType = typeof(string),
                    PropertyInfo = typeof(string).GetTypeInfo()
                }
            };

            var properties2 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property2",
                    Order = 0,
                    PropertyType = typeof(string),
                    PropertyInfo = typeof(string).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = 100,
                    PropertyType = typeof(char),
                    PropertyInfo = typeof(char).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property3",
                    Order = 100,
                    PropertyType = typeof(char),
                    PropertyInfo = typeof(char).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property4",
                    Order = int.MaxValue,
                    PropertyType = typeof(int),
                    PropertyInfo = typeof(int).GetTypeInfo()
                },
            };

            var properties3 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = int.MaxValue,
                    PropertyType = typeof(int),
                    PropertyInfo = typeof(int).GetTypeInfo()
                },
            };


            var propertyDescriptor1Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor1 = propertyDescriptor1Mock.Object;
            propertyDescriptor1Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties1);


            var propertyDescriptor2Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor2 = propertyDescriptor2Mock.Object;
            propertyDescriptor2Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties2);


            var propertyDescriptor3Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor3 = propertyDescriptor2Mock.Object;
            propertyDescriptor3Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties3);

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Type).Returns(typeof(PropertyDescriber));
            var typeMetadata = typeMetadataMock.Object;

            var provider = new PropertyMetadataProvider(new[] { propertyDescriptor2, propertyDescriptor1, propertyDescriptor3 }, new Mock<IMetadatumResolverProvider>().Object);

            var properties = provider.GetProperties(typeMetadata);

            Assert.NotNull(properties.First(x => x.Name == "Property1"));
            Assert.NotNull(properties.First(x => x.Name == "Property2"));
            Assert.NotNull(properties.First(x => x.Name == "Property3"));
            Assert.NotNull(properties.First(x => x.Name == "Property4"));
        }

        [Fact]
        public void ShouldAggregateAndSelectMostImportantProperties()
        {
            var properties1 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = 0,
                    PropertyType = typeof(string),
                    PropertyInfo = typeof(string).GetTypeInfo()
                }
            };

            var properties2 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property2",
                    Order = 0,
                    PropertyType = typeof(PropertyDescriber),
                    PropertyInfo = typeof(PropertyDescriber).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = 100,
                    PropertyType = typeof(char),
                    PropertyInfo = typeof(char).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property3",
                    Order = 100,
                    PropertyType = typeof(uint),
                    PropertyInfo = typeof(uint).GetTypeInfo()
                },
                new PropertyDescriber()
                {
                    Name = "Property4",
                    Order = int.MaxValue,
                    PropertyType = typeof(decimal),
                    PropertyInfo = typeof(decimal).GetTypeInfo()
                },
            };

            var properties3 = new[]
            {
                new PropertyDescriber()
                {
                    Name = "Property1",
                    Order = 200,
                    PropertyType = typeof(int),
                    PropertyInfo = typeof(int).GetTypeInfo()
                },
            };


            var propertyDescriptor1Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor1 = propertyDescriptor1Mock.Object;
            propertyDescriptor1Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties1);


            var propertyDescriptor2Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor2 = propertyDescriptor2Mock.Object;
            propertyDescriptor2Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties2);


            var propertyDescriptor3Mock = new Mock<IPropertyDescriptor>();
            var propertyDescriptor3 = propertyDescriptor3Mock.Object;
            propertyDescriptor3Mock
                .Setup(x => x.Describe(It.IsAny<Type>()))
                .Returns(properties3);

            var typeMetadataMock = new Mock<ITypeMetadata>();
            typeMetadataMock.SetupGet(x => x.Type).Returns(typeof(PropertyDescriber));
            var typeMetadata = typeMetadataMock.Object;


            var provider = new PropertyMetadataProvider(new[] { propertyDescriptor2, propertyDescriptor1, propertyDescriptor3 }, new Mock<IMetadatumResolverProvider>().Object);

            var properties = provider.GetProperties(typeMetadata);

            Assert.Equal(typeof(int), properties.First(x => x.Name == "Property1").PropertyType);
            Assert.Equal(typeof(PropertyDescriber), properties.First(x => x.Name == "Property2").PropertyType);
            Assert.Equal(typeof(uint), properties.First(x => x.Name == "Property3").PropertyType);
            Assert.Equal(typeof(decimal), properties.First(x => x.Name == "Property4").PropertyType);
        }
    }
}
