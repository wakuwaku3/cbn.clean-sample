using System.Collections.Generic;
using System.Linq;
using Cbn.Infrastructure.Common.Foundation.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Tests.Common.Foundation.Extensions
{
    [TestClass]
    public class MemberExtensionsTests
    {
        [TestMethod]
        public void GetPropertyTest()
        {
            var prop = typeof(TestClass).GetProperty(nameof(TestClass.MyProperty));
            Assert.AreEqual(prop, new TestClass().GetProperty(x => x.MyProperty));
        }

        [TestMethod]
        public void GetTest()
        {
            var obj = new TestClass { MyProperty = 5 };
            Assert.AreEqual(5, obj.Get<TestClass, int>(nameof(TestClass.MyProperty)));
            Assert.AreEqual(5, obj.Get<int>(obj.GetProperty(x => x.MyProperty)));
            Assert.AreEqual((object) 5, obj.Get<object>(obj.GetProperty(x => x.MyProperty)));
        }

        [TestMethod]
        public void SetForPropertyTest()
        {
            var obj = new TestClass { MyProperty = 5 };
            obj.Set(nameof(TestClass.MyProperty), 4);
            Assert.AreEqual(4, obj.MyProperty);
            obj.Set(obj.GetProperty(x => x.MyProperty), 3);
            Assert.AreEqual(3, obj.MyProperty);
        }

        [TestMethod]
        public void CastTest()
        {
            var p = new List<string> { "0", "1", }.Select(x => typeof(int).ChangeTypeNullable(x));
            var y = p.Cast(typeof(int));
        }

        class TestClass
        {
            public int MyProperty { get; set; }
        }
    }
}