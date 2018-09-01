using System;
using System.Collections.Generic;
using Cbn.Infrastructure.TestTools.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.SelfTests
{
    [TestClass]
    public class AssertionTests
    {
        /// <summary>
        /// AreEqualTest
        /// </summary>
        [TestMethod]
        public void AreEqualTest()
        {
            Assertion.Is(null, null);
            Assertion.Is("x", "x");
            Assertion.Is(new DateTime?(new DateTime()), new DateTime());
            Assertion.Is(new int?(0), 0);
            Assertion.Is(new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "x" });
            Assertion.Is(new AssertTest1(), new AssertTest1());
            Assertion.Is(new AssertTest2 { MyProperty = new DateTime?(new DateTime()) }, new AssertTest3 { MyProperty = new DateTime() });
            Assertion.Is(new List<string> { "x", "y" }, new List<string> { "x", "y" });
            Assertion.Is(
                new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" } },
                new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" } });
            Assertion.Is(
                new AssertTest5
                {
                    MyProperty1 = "x",
                        MyProperty2 = new List<string> { "x", "y" },
                        MyProperty3 = new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" } }
                },
                new AssertTest5
                {
                    MyProperty1 = "x",
                        MyProperty2 = new List<string> { "x", "y" },
                        MyProperty3 = new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" } }
                });

            Assertion.ThrowsException<AssertException>(() => Assertion.Is("x", "y"));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is("x", null));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(null, "x"));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" }));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = null }));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(new AssertTest1 { MyProperty = null }, new AssertTest1 { MyProperty = "y" }));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(new AssertTest1 { MyProperty = null }, new AssertTest4 { MyProperty2 = "x" }));
            Assertion.ThrowsException<AssertException>(() => Assertion.Is(new List<string> { "x", "y" }, new List<string> { "x", }));
            Assertion.ThrowsException<AssertException>(() =>
            {
                Assertion.Is(
                    new AssertTest5
                    {
                        MyProperty1 = "x",
                            MyProperty2 = new List<string> { "x", "y" },
                            MyProperty3 = new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "y" } }
                    },
                    new AssertTest5
                    {
                        MyProperty1 = "x",
                            MyProperty2 = new List<string> { "x", "y" },
                            MyProperty3 = new List<AssertTest1> { new AssertTest1 { MyProperty = "x" }, new AssertTest1 { MyProperty = "z" } }
                    });
            });
        }

        class AssertTest1
        {
            public string MyProperty { get; set; }
        }
        class AssertTest2
        {
            public DateTime? MyProperty { get; set; }
        }
        class AssertTest3
        {
            public DateTime MyProperty { get; set; }
        }
        class AssertTest4
        {
            public string MyProperty2 { get; set; }
        }
        class AssertTest5
        {
            public string MyProperty1 { get; set; }
            public List<string> MyProperty2 { get; set; }
            public List<AssertTest1> MyProperty3 { get; set; }
        }
    }
}