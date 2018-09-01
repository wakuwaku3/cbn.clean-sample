using System;
using System.Collections;
using System.Collections.Generic;
using Cbn.Infrastructure.Common.Foundation.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Tests.Common.Foundation.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void IsNullableTypeTest()
        {
            Assert.IsTrue(typeof(int?).IsNullableType());
            Assert.IsFalse(typeof(int).IsNullableType());
            Assert.IsFalse(typeof(string).IsNullableType());
            Assert.IsFalse(typeof(TestClass1).IsNullableType());
        }

        [TestMethod]
        public void GetNullableTypeArgumentsTest()
        {
            Assert.AreEqual(typeof(int?).GetNullableTypeArguments(), typeof(int));
            Assert.IsNull(typeof(int).GetNullableTypeArguments());
            Assert.IsNull(typeof(string).GetNullableTypeArguments());
            Assert.IsNull(typeof(TestClass1).GetNullableTypeArguments());
        }

        [TestMethod]
        public void IsEnumerableTest()
        {
            Assert.IsTrue(typeof(string).IsEnumerable());
            Assert.IsFalse(typeof(string).IsEnumerable(typeof(string)));
            Assert.IsTrue(typeof(List<string>).IsEnumerable());
            Assert.IsTrue(typeof(IEnumerable<string>).IsEnumerable());
            Assert.IsTrue(typeof(IEnumerable).IsEnumerable());
            Assert.IsTrue(typeof(TestClass2).IsEnumerable());
        }

        [TestMethod]
        public void GetEnumerableTypeArguments()
        {
            Assert.AreEqual(typeof(string).GetEnumerableTypeArguments(), typeof(char));
            Assert.AreEqual(typeof(List<string>).GetEnumerableTypeArguments(), typeof(string));
            Assert.AreEqual(typeof(IEnumerable<string>).GetEnumerableTypeArguments(), typeof(string));
            Assert.AreEqual(typeof(IEnumerable).GetEnumerableTypeArguments(), typeof(object));
            Assert.AreEqual(typeof(TestClass2).GetEnumerableTypeArguments(), typeof(object));
            Assert.AreEqual(typeof(TestClass3).GetEnumerableTypeArguments(), typeof(string));
        }
        private class TestClass1 { }
        private class TestClass2 : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        private class TestClass3 : IEnumerable<string>
        {
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}