using System;
using Xunit;
using Moq;
using Blacklite.Framework.Metadata.Properties;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;

namespace Metadata.Tests
{

    public class PropertyMetadataTests
    {
        private class Type1 { }

        private class Type2 { }

        [Fact]
        public void ShouldReturnProperties()
        {
            var propertyDescriber = new PropertyDescriber()
            {
                Name = "Property1",
                PropertyInfo = typeof(string).GetTypeInfo(),
                PropertyType = typeof(string)
            };

            var propertyMetadata = new ApplicationPropertyMetadata(new Mock<ITypeMetadata>().Object,
                propertyDescriber,
                Mock.Of<IServiceProvider>(),
                new Mock<IMetadatumResolverProvider>().Object
            );

            Assert.Equal(typeof(string), propertyMetadata.PropertyType);
            Assert.Equal(typeof(string).GetTypeInfo(), propertyMetadata.PropertyInfo);
            Assert.Equal("Property1", propertyMetadata.Name);
        }

        [Fact]
        public void ShouldResolveMetadatums()
        {
            var metadatumResolver1Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver1 = metadatumResolver1Mock.Object;

            var metadatumResolver2Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver2 = metadatumResolver2Mock.Object;

            var metadatumResolver3Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver3 = metadatumResolver3Mock.Object;

            metadatumResolver1Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver1Mock
                .Setup(x => x.CanResolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
                .Returns(false);

            metadatumResolver2Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver2Mock
                .Setup(x => x.CanResolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
                .Returns(true);

            //var returnValue = new Visible(false);
            //metadatumResolver1Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
            //    .Returns(returnValue);

            //var returnValue2 = new Visible(true);
            //metadatumResolver2Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
            //    .Returns(returnValue2);

            //var resolve = new ReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>>(
            //        new Dictionary<Type, IEnumerable<IPropertyMetadatumResolver>>()
            //        {
            //            [typeof(Visible)] = new[] { metadatumResolver1, metadatumResolver3, metadatumResolver2 }
            //        });

            //var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            //var metadatumResolverProvider = metadatumResolverProviderMock.Object;
            //metadatumResolverProviderMock
            //    .SetupGet(x => x.PropertyResolvers)
            //    .Returns(resolve);

            //var propertyDescriber = new PropertyDescriber()
            //{
            //    Name = "Property1",
            //    PropertyInfo = typeof(string).GetTypeInfo(),
            //    PropertyType = typeof(string)
            //};

            //var propertyMetadata = new PropertyMetadata(new Mock<ITypeMetadata>().Object,
            //    propertyDescriber,
            //    Mock.Of<IServiceProvider>(),
            //    metadatumResolverProvider
            //);

            //var resolvedValue = propertyMetadata.Get<Visible>();

            //Assert.True(resolvedValue.Value);

            //var resolvedValue2 = propertyMetadata.Get<Visible>();

            //Assert.Same(resolvedValue, resolvedValue2);
        }

        [Fact]
        public void ShouldResolveFirstResolvedValue()
        {
            var metadatumResolver1Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver1 = metadatumResolver1Mock.Object;

            var metadatumResolver2Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver2 = metadatumResolver2Mock.Object;

            var metadatumResolver3Mock = new Mock<IPropertyMetadatumResolver>();
            var metadatumResolver3 = metadatumResolver3Mock.Object;

            metadatumResolver1Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver1Mock
                .Setup(x => x.CanResolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
                .Returns(true);

            metadatumResolver2Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver2Mock
                .Setup(x => x.CanResolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
                .Returns(true);

            //var returnValue = new Visible(false);
            //metadatumResolver1Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
            //    .Returns(returnValue);

            //var returnValue2 = new Visible(true);
            //metadatumResolver2Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>()))
            //    .Returns(returnValue2);

            //var resolve = new ReadOnlyDictionary<Type, IEnumerable<IPropertyMetadatumResolver>>(
            //        new Dictionary<Type, IEnumerable<IPropertyMetadatumResolver>>()
            //        {
            //            [typeof(Visible)] = new[] { metadatumResolver1, metadatumResolver3, metadatumResolver2 }
            //        });

            //var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            //var metadatumResolverProvider = metadatumResolverProviderMock.Object;
            //metadatumResolverProviderMock
            //    .Setup(x => x.PropertyResolvers)
            //    .Returns(resolve);

            //var propertyDescriber = new PropertyDescriber()
            //{
            //    Name = "Property1",
            //    PropertyInfo = typeof(string).GetTypeInfo(),
            //    PropertyType = typeof(string)
            //};

            //var propertyMetadata = new PropertyMetadata(new Mock<ITypeMetadata>().Object,
            //    null,
            //    propertyDescriber,
            //    metadatumResolverProvider
            //);

            //var resolvedValue = propertyMetadata.Get<Visible>();

            //Assert.False(resolvedValue.Value);

            //var resolvedValue2 = propertyMetadata.Get<Visible>();

            //Assert.Same(resolvedValue, resolvedValue2);
        }
    }
}
