using System;
using Moq;
using Xunit;
using Blacklite.Framework.Metadata.Properties;
using System.Linq;

namespace Metadata.Tests.Properties
{
    public class ReflectionPropertyDescriptorTests
    {
        class TypeA
        {
            public int MyPropertyA { get; set; }
            public string MyPropertyB { get; set; }
            public decimal MyPropertyC { get; set; }
        }

        class TypeB
        {
            public int MyPropertyA { get; set; }
            public decimal MyPropertyC { get; set; }
        }

        class TypeC
        {
            public string MyPropertyB { get; set; }
            public decimal MyPropertyC { get; set; }
        }

        [Fact]
        public void DescribesPropertiesOfTypes()
        {
            var desciptor = new ReflectionPropertyDescriptor();
            var propertiesOfTypeA = desciptor.Describe(typeof(TypeA));

            Assert.True(propertiesOfTypeA.Where(x => x.Name == nameof(TypeA.MyPropertyA)).Any());
            Assert.True(propertiesOfTypeA.Where(x => x.Name == nameof(TypeA.MyPropertyB)).Any());
            Assert.True(propertiesOfTypeA.Where(x => x.Name == nameof(TypeA.MyPropertyC)).Any());

            var propertiesOfTypeB = desciptor.Describe(typeof(TypeB));

            Assert.True(propertiesOfTypeB.Where(x => x.Name == nameof(TypeB.MyPropertyA)).Any());
            Assert.False(propertiesOfTypeB.Where(x => x.Name == nameof(TypeA.MyPropertyB)).Any());
            Assert.True(propertiesOfTypeB.Where(x => x.Name == nameof(TypeB.MyPropertyC)).Any());

            var propertiesOfTypeC = desciptor.Describe(typeof(TypeC));

            Assert.False(propertiesOfTypeC.Where(x => x.Name == nameof(TypeA.MyPropertyA)).Any());
            Assert.True(propertiesOfTypeC.Where(x => x.Name == nameof(TypeC.MyPropertyB)).Any());
            Assert.True(propertiesOfTypeC.Where(x => x.Name == nameof(TypeC.MyPropertyC)).Any());
        }
    }
}
