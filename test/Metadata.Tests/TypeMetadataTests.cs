using System;
using Xunit;
using Moq;
using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;

namespace Metadata.Tests
{

    public class TypeMetadataTests
    {
        private class Type1 { }

        private class Type2 { }

        [Fact]
        public void ShouldReturnProperties()
        {
            var propertyMetadataProviderMock = new Mock<IPropertyMetadataProvider>();
            var propertyMetadataProvider = propertyMetadataProviderMock.Object;

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

            //            var provider = new MetadataProvider(propertyMetadataProvider, new Mock<IMetadatumResolverProvider>().Object);

            var type1TypeMetadata = new ApplicationTypeMetadata(typeof(Type1), Mock.Of<IServiceProvider>(), propertyMetadataProvider, new Mock<IMetadatumResolverProvider>().Object);

            Assert.Contains(propertyMetadata1, type1TypeMetadata.Properties);
            Assert.Contains(propertyMetadata2, type1TypeMetadata.Properties);
            Assert.Contains(propertyMetadata3, type1TypeMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata4, type1TypeMetadata.Properties);


            var type2TypeMetadata = new ApplicationTypeMetadata(typeof(Type2), Mock.Of<IServiceProvider>(), propertyMetadataProvider, new Mock<IMetadatumResolverProvider>().Object);

            Assert.Contains(propertyMetadata1, type2TypeMetadata.Properties);
            Assert.DoesNotContain(propertyMetadata2, type2TypeMetadata.Properties);
            Assert.Contains(propertyMetadata3, type2TypeMetadata.Properties);
            Assert.Contains(propertyMetadata4, type2TypeMetadata.Properties);
        }

        [Fact]
        public void ShouldResolveMetadatums()
        {
            var propertyMetadataProviderMock = new Mock<IPropertyMetadataProvider>();
            var propertyMetadataProvider = propertyMetadataProviderMock.Object;

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type1")))
                .Returns(Enumerable.Empty<IPropertyMetadata>());

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type2")))
                .Returns(Enumerable.Empty<IPropertyMetadata>());

            var metadatumResolver1Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver1 = metadatumResolver1Mock.Object;

            var metadatumResolver2Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver2 = metadatumResolver2Mock.Object;

            var metadatumResolver3Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver3 = metadatumResolver3Mock.Object;

            metadatumResolver1Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver1Mock
                .Setup(x => x.CanResolve(It.Is<IMetadatumResolutionContext<ITypeMetadata>>(z => z.MetadatumType ==typeof(Visible))))
                .Returns(false);

            metadatumResolver2Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver2Mock
                .Setup(x => x.CanResolve(It.Is<IMetadatumResolutionContext<ITypeMetadata>>(z => z.MetadatumType == typeof(Visible))))
                .Returns(true);

            //var returnValue = new Visible(false);
            //metadatumResolver1Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>()))
            //    .Returns(returnValue);

            //var returnValue2 = new Visible(true);
            //metadatumResolver2Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>()))
            //    .Returns(returnValue2);

            //var resolve = new ReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>>(
            //        new Dictionary<Type, IEnumerable<ITypeMetadatumResolver>>()
            //        {
            //            [typeof(Visible)] = new[] { metadatumResolver1, metadatumResolver3, metadatumResolver2 }
            //        });

            //var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            //var metadatumResolverProvider = metadatumResolverProviderMock.Object;
            //metadatumResolverProviderMock
            //    .Setup(x => x.TypeResolvers)
            //    .Returns(resolve);


            //var typeMetadata = new TypeMetadata(typeof(Type1), null, propertyMetadataProvider, metadatumResolverProvider);

            //var resolvedValue = typeMetadata.Get<Visible>();

            //Assert.True(resolvedValue.Value);

            //var resolvedValue2 = typeMetadata.Get<Visible>();

            //Assert.Same(resolvedValue, resolvedValue2);
        }

        [Fact]
        public void ShouldResolveFirstResolvedValue()
        {
            var propertyMetadataProviderMock = new Mock<IPropertyMetadataProvider>();
            var propertyMetadataProvider = propertyMetadataProviderMock.Object;

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type1")))
                .Returns(Enumerable.Empty<IPropertyMetadata>());

            propertyMetadataProviderMock
                .Setup(x => x.GetApplicationProperties(It.Is<IApplicationTypeMetadata>(z => z.Name == "Type2")))
                .Returns(Enumerable.Empty<IPropertyMetadata>());

            var metadatumResolver1Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver1 = metadatumResolver1Mock.Object;

            var metadatumResolver2Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver2 = metadatumResolver2Mock.Object;

            var metadatumResolver3Mock = new Mock<ITypeMetadatumResolver>();
            var metadatumResolver3 = metadatumResolver3Mock.Object;

            metadatumResolver1Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver1Mock
                .Setup(x => x.CanResolve(It.Is<IMetadatumResolutionContext<ITypeMetadata>>(z => z.MetadatumType == typeof(Visible))))
                .Returns(true);

            metadatumResolver2Mock
                .Setup(x => x.GetMetadatumType())
                .Returns(typeof(Visible));

            metadatumResolver2Mock
                .Setup(x => x.CanResolve(It.Is<IMetadatumResolutionContext<ITypeMetadata>>(z => z.MetadatumType == typeof(Visible))))
                .Returns(true);

            //var returnValue = new Visible(false);
            //metadatumResolver1Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>()))
            //    .Returns(returnValue);

            //var returnValue2 = new Visible(true);
            //metadatumResolver2Mock
            //    .Setup(x => x.Resolve<Visible>(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>()))
            //    .Returns(returnValue2);

            //var resolve = new ReadOnlyDictionary<Type, IEnumerable<ITypeMetadatumResolver>>(
            //        new Dictionary<Type, IEnumerable<ITypeMetadatumResolver>>()
            //        {
            //            [typeof(Visible)] = new[] { metadatumResolver1, metadatumResolver3, metadatumResolver2 }
            //        });

            //var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            //var metadatumResolverProvider = metadatumResolverProviderMock.Object;
            //metadatumResolverProviderMock
            //    .Setup(x => x.TypeResolvers)
            //    .Returns(resolve);

            //var typeMetadata = new TypeMetadata(typeof(Type1), null, propertyMetadataProvider, metadatumResolverProvider);

            //var resolvedValue = typeMetadata.Get<Visible>();

            //Assert.False(resolvedValue.Value);

            //var resolvedValue2 = typeMetadata.Get<Visible>();

            //Assert.Same(resolvedValue, resolvedValue2);
        }
    }
}
