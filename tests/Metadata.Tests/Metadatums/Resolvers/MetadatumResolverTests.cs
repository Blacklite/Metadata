using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Metadata.Tests.Metadatums.Resolvers
{
    public class MetadatumResolverTests
    {
        private class Scoped : IMetadatum { }
        private class Simple : IMetadatum { }


        public abstract class TypeMetadatumResolver : ITypeMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context);
            public abstract Type GetMetadatumType();
            public abstract IMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context);
        }

        [Fact]
        public void TypeMetadatumProviderReturnsAMetadatum()
        {
            var applicationServiceProvider = Mock.Of<IServiceProvider>();
            var serviceProvider = Mock.Of<IServiceProvider>();
            var metadataPropertyProvider = Mock.Of<IPropertyMetadataProvider>();
            var scoped = new Scoped();
            var simple = new Simple();

            var scopedResolver = new Mock<TypeMetadatumResolver>();
            scopedResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(true);
            scopedResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(new Scoped());

            var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumResolverProviderMock.Setup(x => x.TypeResolvers).Returns(
                new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<ITypeMetadatumResolver, ITypeMetadata>(scopedResolver.Object) }
                }));
            var metadatumResolverProvider = metadatumResolverProviderMock.Object;

            var applicationMetdataMock = new Mock<IApplicationTypeMetadata>();
            applicationMetdataMock.Setup(x => x.Get<Simple>()).Returns(simple);

            var applicationMetdata = applicationMetdataMock.Object;
            var metadata = new TypeMetadata(applicationMetdata, serviceProvider, metadataPropertyProvider, metadatumResolverProvider);

            var scopedResult = metadata.Get<Scoped>();
            Assert.NotEqual(scoped, scopedResult);
            Assert.IsType<Scoped>(scopedResult);
            var scopedResult2 = metadata.Get<Scoped>();
            Assert.Same(scopedResult, scopedResult2);

            var simpleResult = metadata.Get<Simple>();
            Assert.Same(simple, simpleResult);
            var simpleResult2 = metadata.Get<Simple>();
            Assert.Same(simpleResult, simpleResult2);
        }


        public abstract class ApplicationTypeMetadatumResolver : IApplicationTypeMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract bool CanResolve(IMetadatumResolutionContext<ITypeMetadata> context);
            public abstract Type GetMetadatumType();
            public abstract IMetadatum Resolve(IMetadatumResolutionContext<ITypeMetadata> context);
        }

        [Fact]
        public void ApplicationTypeMetadatumProviderReturnsAMetadatum()
        {
            var serviceProvider = Mock.Of<IServiceProvider>();
            var metadataPropertyProvider = Mock.Of<IPropertyMetadataProvider>();
            var scoped = new Scoped();
            var simple = new Simple();

            var scopedResolver = new Mock<ApplicationTypeMetadatumResolver>();
            scopedResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(true);
            scopedResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(scoped);

            var simpleResolver = new Mock<ApplicationTypeMetadatumResolver>();
            simpleResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(true);
            simpleResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<ITypeMetadata>>())).Returns(simple);

            var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumResolverProviderMock.Setup(x => x.ApplicationTypeResolvers).Returns(new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<IApplicationTypeMetadatumResolver, ITypeMetadata>(scopedResolver.Object) },
                    [typeof(Simple)] = new[] { new MetadatumResolverDescriptor<IApplicationTypeMetadatumResolver, ITypeMetadata>(simpleResolver.Object) }
                }));
            var metadatumResolverProvider = metadatumResolverProviderMock.Object;

            var metadatumDescriberProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumDescriberProviderMock.Setup(x => x.ApplicationTypeResolvers).Returns(
                new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<ITypeMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<IApplicationTypeMetadatumResolver, ITypeMetadata>(scopedResolver.Object), },
                    [typeof(Simple)] = new[] { new MetadatumResolverDescriptor<IApplicationTypeMetadatumResolver, ITypeMetadata>(simpleResolver.Object), }
                }));

            var metadatumDescriberProvider = metadatumDescriberProviderMock.Object;

            var provider = new ApplicationTypeMetadata(typeof(MetadatumResolverTests), serviceProvider, metadataPropertyProvider, metadatumDescriberProvider);

            var scopedResult = provider.Get<Scoped>();
            Assert.Same(scoped, scopedResult);

            var simpleResult = provider.Get<Simple>();
            Assert.Same(simple, simpleResult);
        }

        public abstract class PropertyMetadatumResolver : IPropertyMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context);
            public abstract Type GetMetadatumType();
            public abstract IMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context);
        }

        [Fact]
        public void PropertyMetadatumProviderReturnsAMetadatum()
        {
            var applicationServiceProvider = Mock.Of<IServiceProvider>();
            var serviceProvider = Mock.Of<IServiceProvider>();
            var metadataPropertyProvider = Mock.Of<IPropertyMetadataProvider>();
            var scoped = new Scoped();
            var simple = new Simple();

            var scopedResolver = new Mock<PropertyMetadatumResolver>();
            scopedResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(true);
            scopedResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(new Scoped());

            var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumResolverProviderMock.Setup(x => x.PropertyResolvers).Returns(
                new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<IPropertyMetadatumResolver, IPropertyMetadata>(scopedResolver.Object) }
                }));
            var metadatumResolverProvider = metadatumResolverProviderMock.Object;

            var applicationMetdataMock = new Mock<IPropertyMetadata>();
            applicationMetdataMock.Setup(x => x.Get<Simple>()).Returns(simple);
            var applicationMetdata = applicationMetdataMock.Object;

            var parentMetadataMock = new Mock<ITypeMetadata>();
            var parentMetadata = parentMetadataMock.Object;
            var metadata = new PropertyMetadata(applicationMetdata, parentMetadata, serviceProvider, metadatumResolverProvider);

            var scopedResult = metadata.Get<Scoped>();
            Assert.NotEqual(scoped, scopedResult);
            Assert.IsType<Scoped>(scopedResult);
            var scopedResult2 = metadata.Get<Scoped>();
            Assert.Same(scopedResult, scopedResult2);

            var simpleResult = metadata.Get<Simple>();
            Assert.Same(simple, simpleResult);
            var simpleResult2 = metadata.Get<Simple>();
            Assert.Same(simpleResult, simpleResult2);
        }


        public abstract class ApplicationPropertyMetadatumResolver : IApplicationPropertyMetadatumResolver
        {
            public abstract int Priority { get; }

            public abstract bool CanResolve(IMetadatumResolutionContext<IPropertyMetadata> context);
            public abstract Type GetMetadatumType();
            public abstract IMetadatum Resolve(IMetadatumResolutionContext<IPropertyMetadata> context);
        }

        [Fact]
        public void ApplicationPropertyMetadatumProviderReturnsAMetadatum()
        {
            var serviceProvider = Mock.Of<IServiceProvider>();
            var metadataPropertyProvider = Mock.Of<IPropertyMetadataProvider>();
            var scoped = new Scoped();
            var simple = new Simple();

            var scopedResolver = new Mock<ApplicationPropertyMetadatumResolver>();
            scopedResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(true);
            scopedResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(scoped);

            var simpleResolver = new Mock<ApplicationPropertyMetadatumResolver>();
            simpleResolver.Setup(x => x.CanResolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(true);
            simpleResolver.Setup(x => x.Resolve(It.IsAny<IMetadatumResolutionContext<IPropertyMetadata>>())).Returns(simple);

            var metadatumResolverProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumResolverProviderMock.Setup(x => x.ApplicationPropertyResolvers).Returns(new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<IApplicationPropertyMetadatumResolver, IPropertyMetadata>(scopedResolver.Object) },
                    [typeof(Simple)] = new[] { new MetadatumResolverDescriptor<IApplicationPropertyMetadatumResolver, IPropertyMetadata>(simpleResolver.Object) }
                }));
            var metadatumResolverProvider = metadatumResolverProviderMock.Object;

            var metadatumDescriberProviderMock = new Mock<IMetadatumResolverProvider>();
            metadatumDescriberProviderMock.Setup(x => x.ApplicationPropertyResolvers).Returns(
                new ReadOnlyDictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>(
                new Dictionary<Type, IEnumerable<IMetadatumResolverDescriptor<IPropertyMetadata>>>()
                {
                    [typeof(Scoped)] = new[] { new MetadatumResolverDescriptor<IApplicationPropertyMetadatumResolver, IPropertyMetadata>(scopedResolver.Object), },
                    [typeof(Simple)] = new[] { new MetadatumResolverDescriptor<IApplicationPropertyMetadatumResolver, IPropertyMetadata>(simpleResolver.Object), }
                }));

            var metadatumDescriberProvider = metadatumDescriberProviderMock.Object;

            var parentMetadataMock = new Mock<ITypeMetadata>();
            var parentMetadata = parentMetadataMock.Object;

            var propertyDescriberMock = new Mock<IPropertyDescriber>();
            var propertyDescriber = propertyDescriberMock.Object;

            var provider = new ApplicationPropertyMetadata(parentMetadata, propertyDescriber, serviceProvider, metadatumDescriberProvider);

            var scopedResult = provider.Get<Scoped>();
            Assert.Same(scoped, scopedResult);

            var simpleResult = provider.Get<Simple>();
            Assert.Same(simple, simpleResult);
        }
    }
}
