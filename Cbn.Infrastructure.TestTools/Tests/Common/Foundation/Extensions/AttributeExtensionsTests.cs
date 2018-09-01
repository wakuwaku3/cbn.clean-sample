using Cbn.Infrastructure.Common.Foundation.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Tests.Common.Foundation.Extensions
{
    [TestClass]
    public class AttributeExtensionsTests
    {
        [TestMethod]
        public void GetAttributeValueTest()
        {
            Assert.AreEqual("class", typeof(TestClass1).GetAttributeValue<System.ComponentModel.DescriptionAttribute, string>(x => x.Description));
            Assert.AreEqual("prop", typeof(TestClass1).GetProperty(nameof(TestClass1.MyProperty)).GetAttributeValue<System.ComponentModel.DescriptionAttribute, string>(x => x.Description));
            Assert.AreEqual(null, typeof(TestClass2).GetAttributeValue<System.ComponentModel.DescriptionAttribute, string>(x => x.Description));
            Assert.AreEqual(null, typeof(TestClass2).GetProperty(nameof(TestClass2.MyProperty)).GetAttributeValue<System.ComponentModel.DescriptionAttribute, string>(x => x.Description));
        }

        [System.ComponentModel.Description("class")]
        class TestClass1
        {
            [System.ComponentModel.Description("prop")]
            public int MyProperty { get; set; }
        }
        class TestClass2
        {
            public int MyProperty { get; set; }
        }
    }
}