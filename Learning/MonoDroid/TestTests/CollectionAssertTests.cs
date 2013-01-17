using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTests
{
    [TestClass]
    public class CollectionAssertTests : TestsBase
    {
        private struct TestStruct
        {
        }

        [TestMethod]
        public void TestAllItemsAreInstancesOfType()
        {
            CollectionAssert.AllItemsAreInstancesOfType(new List<object> {"test", "test2", "test3"}, typeof (string));
            CollectionAssert.AllItemsAreInstancesOfType(new List<object> {"test", "test2", "test3"}, typeof (String));
            ExpectFail(() => CollectionAssert.AllItemsAreInstancesOfType(new List<object> {"test", "test2", "test3", null}, typeof (string)));
            ExpectFail(() => CollectionAssert.AllItemsAreInstancesOfType(new List<object> {null}, typeof (string)));
            CollectionAssert.AllItemsAreInstancesOfType(new List<object>(), typeof (string));
            CollectionAssert.AllItemsAreInstancesOfType(new List<TestStruct> {new TestStruct()}, typeof (TestStruct));
            CollectionAssert.AllItemsAreInstancesOfType(new List<TestStruct> {new TestStruct()}, typeof (ValueType));
            CollectionAssert.AllItemsAreInstancesOfType(new List<TestStruct> {new TestStruct()}, typeof (object));

            ExpectFail(() => CollectionAssert.AllItemsAreInstancesOfType(null, typeof (object)));
            ExpectFail(() => CollectionAssert.AllItemsAreInstancesOfType(new List<object>(), null));

            ExpectFail(() => CollectionAssert.AllItemsAreInstancesOfType(new List<object> {"test", 5}, typeof (string)));
        }

        [TestMethod]
        public void TestAllItemsAreNotNull()
        {
            CollectionAssert.AllItemsAreNotNull(new List<object> {"test", "test2", "test3"});
            CollectionAssert.AllItemsAreNotNull(new List<object> {"test"});
            CollectionAssert.AllItemsAreNotNull(new List<object> {});
            ExpectFail(() => CollectionAssert.AllItemsAreNotNull(new List<object> {"test", "test2", null}));
            ExpectFail(() => CollectionAssert.AllItemsAreNotNull(new List<object> {null}));
            ExpectFail(() => CollectionAssert.AllItemsAreNotNull(null));
        }

        [TestMethod]
        public void TestAllItemsAreUnique()
        {
            CollectionAssert.AllItemsAreUnique(new List<object> {"test", "test2", "test3"});
            ExpectFail(() => CollectionAssert.AllItemsAreUnique(new List<object> {"test", "test", "test3"}));
            CollectionAssert.AllItemsAreUnique(new List<object> {"test"});
            CollectionAssert.AllItemsAreUnique(new List<object> {});
            ExpectFail(() => CollectionAssert.AllItemsAreUnique(null));
        }

        [TestMethod]
        public void TestAreEqual()
        {
            CollectionAssert.AreEqual(new List<object> {"test", "test2", "test3"},
                                      new List<object> {"test", "test2", "test3"});
            ExpectFail(() =>
                       CollectionAssert.AreEqual(new List<object> {"test", "test2", "test3"},
                                                 new List<object> {"test", "test4", "test3"}));
            ExpectFail(() =>
                       CollectionAssert.AreEqual(new List<object> {"test", "test2", "test3"},
                                                 new List<object> {"test", "test3", "test2"}));
        }

        [TestMethod]
        public void TestAreNotEqual()
        {
            ExpectFail(() =>
                       CollectionAssert.AreNotEqual(new List<object> {"test", "test2", "test3"},
                                                    new List<object> {"test", "test2", "test3"}));
            CollectionAssert.AreNotEqual(new List<object> {"test", "test2", "test3"},
                                         new List<object> {"test", "test4", "test3"});
            CollectionAssert.AreNotEqual(new List<object> {"test", "test2", "test3"},
                                         new List<object> {"test", "test3", "test2"});
        }

        [TestMethod]
        public void TestAreEquivalent()
        {
            CollectionAssert.AreEquivalent(new List<object> {"test", "test2", "test3"},
                                           new List<object> {"test", "test2", "test3"});
            ExpectFail(() =>
                       CollectionAssert.AreEquivalent(new List<object> {"test", "test2", "test3"},
                                                      new List<object> {"test", "test4", "test3"}));
            CollectionAssert.AreEquivalent(new List<object> {"test", "test2", "test3"},
                                           new List<object> {"test", "test3", "test2"});
        }

        [TestMethod]
        public void TestAreNotEquivalent()
        {
            ExpectFail(() =>
                       CollectionAssert.AreNotEquivalent(new List<object> {"test", "test2", "test3"},
                                                         new List<object> {"test", "test2", "test3"}));
            CollectionAssert.AreNotEquivalent(new List<object> {"test", "test2", "test3"},
                                              new List<object> {"test", "test4", "test3"});
            ExpectFail(() =>
                       CollectionAssert.AreNotEquivalent(new List<object> {"test", "test2", "test3"},
                                                         new List<object> {"test", "test3", "test2"}));
        }

        [TestMethod]
        public void TestContains()
        {
            CollectionAssert.Contains(new List<object> {"test", "test2", "test3"}, "test2");
            ExpectFail(() => CollectionAssert.Contains(new List<object> {"test", "test2", "test3"}, "test4"));
            ExpectFail(() => CollectionAssert.Contains(new List<object> {"test", "test2", "test3"}, null));
            ExpectFail(() => CollectionAssert.Contains(null, "test2"));
        }

        [TestMethod]
        public void TestDoesNotContain()
        {
            ExpectFail(() => CollectionAssert.DoesNotContain(new List<object> {"test", "test2", "test3"}, "test2"));
            CollectionAssert.DoesNotContain(new List<object> {"test", "test2", "test3"}, "test4");
            CollectionAssert.DoesNotContain(new List<object> {"test", "test2", "test3"}, null);
            ExpectFail(() => CollectionAssert.DoesNotContain(null, "test2"));
        }

        [TestMethod]
        public void TestIsSubsetOf()
        {
            CollectionAssert.IsSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {"test", "test2", "test3"});
            CollectionAssert.IsSubsetOf(new List<object> {"test", "test2"}, new List<object> {"test", "test2", "test3"});
            CollectionAssert.IsSubsetOf(new List<object> {"test"}, new List<object> {"test", "test2", "test3"});
            CollectionAssert.IsSubsetOf(new List<object> {}, new List<object> {"test", "test2", "test3"});
            ExpectFail(() => CollectionAssert.IsSubsetOf(null, new List<object> {"test", "test2", "test3"}));

            ExpectFail(() => CollectionAssert.IsSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {"test", "test2"}));
            ExpectFail(() => CollectionAssert.IsSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {}));
            ExpectFail(() => CollectionAssert.IsSubsetOf(new List<object> {"test", "test2", "test3"}, null));
        }

        [TestMethod]
        public void TestIsNotSubsetOf()
        {
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {"test", "test2", "test3"}));
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(new List<object> {"test", "test2"}, new List<object> {"test", "test2", "test3"}));
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(new List<object> {"test"}, new List<object> {"test", "test2", "test3"}));
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(new List<object> {}, new List<object> {"test", "test2", "test3"}));
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(null, new List<object> {"test", "test2", "test3"}));

            CollectionAssert.IsNotSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {"test", "test2"});
            CollectionAssert.IsNotSubsetOf(new List<object> {"test", "test2", "test3"}, new List<object> {});
            ExpectFail(() => CollectionAssert.IsNotSubsetOf(new List<object> {"test", "test2", "test3"}, null));
        }
    }
}