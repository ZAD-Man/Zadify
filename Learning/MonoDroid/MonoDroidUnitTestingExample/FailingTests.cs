using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonoDroidUnitTesting
{
    [TestClass]
    public class FailingTests
    {
        [TestMethod]
        public void TestSimpleStackTrace()
        {
            Assert.Fail("Booyah - this failed.");
        }

        [TestMethod]
        public void TestComplexStackTrace()
        {
            // CollectionAssert.AreEqual() uses some helper function. Test whether the stack trace is still clean.
            CollectionAssert.AreEqual(new List<string> {"test"}, new List<string> {"test2"});
        }
    }
}