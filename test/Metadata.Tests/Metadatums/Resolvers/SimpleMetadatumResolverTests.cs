using Blacklite.Framework.Metadata;
using Blacklite.Framework.Metadata.Metadatums;
using Blacklite.Framework.Metadata.Metadatums.Resolvers;
using Blacklite.Framework.Metadata.Properties;
using Moq;
using System;
using Xunit;

namespace Metadata.Tests.Metadatums.Resolvers
{
    public class SimpleMetadatumResolverTests
    {
        public class Scoped : IMetadatum { }
        public class NotScoped : IMetadatum { }

        private class PrivateSimpleTypeMetadatumResolver : SimpleTypeMetadatumResolver<Scoped>
        {
            public override Scoped Resolve(IMetadatumResolutionContext<ITypeMetadata> context)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void SimpleTypeMetadatumResolverOnlyResolversGenericMetadatumTypeParameter()
        {
            var resolver = (ITypeMetadatumResolver)new PrivateSimpleTypeMetadatumResolver();

            Assert.True(resolver.CanResolve(new TypeMetadatumResolutionContext(null, Mock.Of<ITypeMetadata>(), typeof(Scoped))));
            Assert.False(resolver.CanResolve(new TypeMetadatumResolutionContext(null, Mock.Of<ITypeMetadata>(), typeof(NotScoped))));
        }

        [Fact]
        public void SimpleApplicationTypeMetadatumResolverOnlyResolversGenericMetadatumTypeParameter()
        {
            var resolver = (IApplicationTypeMetadatumResolver)new PrivateSimpleTypeMetadatumResolver();

            Assert.True(resolver.CanResolve(new TypeMetadatumResolutionContext(null, Mock.Of<ITypeMetadata>(), typeof(Scoped))));
            Assert.False(resolver.CanResolve(new TypeMetadatumResolutionContext(null, Mock.Of<ITypeMetadata>(), typeof(NotScoped))));
        }

        private class PrivateSimplePropertyMetadatumResolver : SimplePropertyMetadatumResolver<Scoped>
        {
            public override Scoped Resolve(IMetadatumResolutionContext<IPropertyMetadata> context)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void SimplePropertyMetadatumResolverOnlyResolversGenericMetadatumTypeParameter()
        {
            var resolver = (IPropertyMetadatumResolver)new PrivateSimplePropertyMetadatumResolver();

            Assert.True(resolver.CanResolve(new PropertyMetadatumResolutionContext(null, Mock.Of<IPropertyMetadata>(), typeof(Scoped))));
            Assert.False(resolver.CanResolve(new PropertyMetadatumResolutionContext(null, Mock.Of<IPropertyMetadata>(), typeof(NotScoped))));
        }

        [Fact]
        public void SimpleApplicationPropertyMetadatumResolverOnlyResolversGenericMetadatumTypeParameter()
        {
            var resolver = (IApplicationPropertyMetadatumResolver)new PrivateSimplePropertyMetadatumResolver();

            Assert.True(resolver.CanResolve(new PropertyMetadatumResolutionContext(null, Mock.Of<IPropertyMetadata>(), typeof(Scoped))));
            Assert.False(resolver.CanResolve(new PropertyMetadatumResolutionContext(null, Mock.Of<IPropertyMetadata>(), typeof(NotScoped))));
        }
    }
}
