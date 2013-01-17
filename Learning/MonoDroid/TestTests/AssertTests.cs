using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;

namespace TestTests
{
    [TestClass]
    public class AssertTestsWithALongName : TestsBase
    {
        private class EqualityClass
        {
            private readonly string m_text;

            public EqualityClass(string text)
            {
                this.m_text = text;
            }

            public override bool Equals(object obj)
            {
                EqualityClass other = obj as EqualityClass;
                if (object.ReferenceEquals(other, null))
                {
                    return false;
                }

                return this.m_text == other.m_text;
            }

            public override int GetHashCode()
            {
                return this.m_text.GetHashCode();
            }
        }

        private struct TestStruct
        {
        }

        [TestMethod]
        public void TestFail()
        {
            try
            {
                Assert.Fail();
                throw new Exception("Should not happen");
            }
            catch (AssertFailedException)
            {
                // Expected
            }
        }

        [TestMethod]
        public void TestInconclusive()
        {
            try
            {
                Assert.Inconclusive();
                throw new Exception("Should not happen");
            }
            catch (AssertInconclusiveException)
            {
                // Expected
            }
        }

        [TestMethod]
        public void TestAreEqual()
        {
            Assert.AreEqual(null, null);

            Assert.AreEqual(true, true);
            Assert.AreEqual(false, false);

            Assert.AreEqual(42, 42);
            Assert.AreEqual(42, 42L);

            Assert.AreEqual('c', 'c');

            Assert.AreEqual(0.333333333f, 1.0f/3, 0.0000001f);
            Assert.AreEqual(0.333333333, 1.0/3, 0.0000001);

            Assert.AreEqual("abcäöü", "abcäöü");
            Assert.AreEqual("abcäöü", "abcäöü", ignoreCase: false);
            Assert.AreEqual("abcäöü", "abcÄÖÜ", ignoreCase: true);
            Assert.AreEqual("abcäöü", "abcÄÖÜ", true, CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(new EqualityClass("test"), new EqualityClass("test"));
            Assert.AreEqual((object) new EqualityClass("test"), (object) new EqualityClass("test"));
        }

        [TestMethod]
        public void TestAreEqualFails()
        {
            ExpectFail(() => Assert.AreEqual(true, false));
            ExpectFail(() => Assert.AreEqual(false, true));

            ExpectFail(() => Assert.AreEqual(42, 41));
            ExpectFail(() => Assert.AreEqual(42, 41L));

            ExpectFail(() => Assert.AreEqual('c', 'd'));

            ExpectFail(() => Assert.AreEqual(0.4f, 1.0f/3, 0.0000001f));
            ExpectFail(() => Assert.AreEqual(0.4, 1.0/3, 0.0000001));

            ExpectFail(() => Assert.AreEqual("abcäöü", "abcäöÜ"));
            ExpectFail(() => Assert.AreEqual("abcäöü", "abcäöÜ", ignoreCase: false));
            ExpectFail(() => Assert.AreEqual("abcäöü", "abdÄÖÜ", ignoreCase: true));
            ExpectFail(() => Assert.AreEqual("abcäöü", "abdÄÖÜ", true, CultureInfo.GetCultureInfo("de")));

            ExpectFail(() => Assert.AreEqual(null, new EqualityClass("test")));
            ExpectFail(() => Assert.AreEqual(new EqualityClass("test"), null));
            ExpectFail(() => Assert.AreEqual(new EqualityClass("test"), new EqualityClass("test2")));
        }

        [TestMethod]
        public void TestAreNotEqual()
        {
            Assert.AreNotEqual(true, false);
            Assert.AreNotEqual(false, true);

            Assert.AreNotEqual(42, 41);
            Assert.AreNotEqual(42, 41L);

            Assert.AreNotEqual('c', 'd');

            Assert.AreNotEqual(0.4f, 1.0f/3, 0.0000001f);
            Assert.AreNotEqual(0.4, 1.0/3, 0.0000001);

            Assert.AreNotEqual("abcäöü", "abcäöÜ");
            Assert.AreNotEqual("abcäöü", "abcäöÜ", ignoreCase: false);
            Assert.AreNotEqual("abcäöü", "abdÄÖÜ", ignoreCase: true);
            Assert.AreNotEqual("abcäöü", "abdÄÖÜ", true, CultureInfo.GetCultureInfo("de"));

            Assert.AreNotEqual(null, new EqualityClass("test"));
            Assert.AreNotEqual(new EqualityClass("test"), null);
            Assert.AreNotEqual(new EqualityClass("test"), new EqualityClass("test2"));
        }

        [TestMethod]
        public void TestAreNotEqualFail()
        {
            ExpectFail(() => Assert.AreNotEqual(null, null));

            ExpectFail(() => Assert.AreNotEqual(true, true));
            ExpectFail(() => Assert.AreNotEqual(false, false));

            ExpectFail(() => Assert.AreNotEqual(42, 42));
            ExpectFail(() => Assert.AreNotEqual(42, 42L));

            ExpectFail(() => Assert.AreNotEqual('c', 'c'));

            ExpectFail(() => Assert.AreNotEqual(0.333333333f, 1.0f/3, 0.0000001f));
            ExpectFail(() => Assert.AreNotEqual(0.333333333, 1.0/3, 0.0000001));

            ExpectFail(() => Assert.AreNotEqual("abcäöü", "abcäöü"));
            ExpectFail(() => Assert.AreNotEqual("abcäöü", "abcäöü", ignoreCase: false));
            ExpectFail(() => Assert.AreNotEqual("abcäöü", "abcÄÖÜ", ignoreCase: true));
            ExpectFail(() => Assert.AreNotEqual("abcäöü", "abcÄÖÜ", true, CultureInfo.GetCultureInfo("de")));

            ExpectFail(() => Assert.AreNotEqual(new EqualityClass("test"), new EqualityClass("test")));
            ExpectFail(() => Assert.AreNotEqual((object) new EqualityClass("test"), (object) new EqualityClass("test")));
        }

        [TestMethod]
        public void TestAreSame()
        {
            EqualityClass v1 = new EqualityClass("test");
            EqualityClass v2 = new EqualityClass("test");

            Assert.AreSame(v1, v1);
            Assert.AreSame(v2, v2);
            ExpectFail(() => Assert.AreSame(v1, v2));
        }

        [TestMethod]
        public void TestAreNotSame()
        {
            EqualityClass v1 = new EqualityClass("test");
            EqualityClass v2 = new EqualityClass("test");

            ExpectFail(() => Assert.AreNotSame(v1, v1));
            ExpectFail(() => Assert.AreNotSame(v2, v2));
            Assert.AreNotSame(v1, v2);
        }

        [TestMethod]
        public void TestIsNull()
        {
            EqualityClass v1 = new EqualityClass("test");
            EqualityClass v2 = null;

            Assert.IsNull(null);
            Assert.IsNull(v2);
            ExpectFail(() => Assert.IsNull(v1));
        }

        [TestMethod]
        public void TestIsNotNull()
        {
            EqualityClass v1 = new EqualityClass("test");
            EqualityClass v2 = null;

            ExpectFail(() => Assert.IsNotNull(null));
            ExpectFail(() => Assert.IsNotNull(v2));
            Assert.IsNotNull(v1);
        }

        [TestMethod]
        public void TestBoolean()
        {
            Assert.IsTrue(true);
            ExpectFail(() => Assert.IsTrue(false));

            Assert.IsFalse(false);
            ExpectFail(() => Assert.IsFalse(true));
        }

        [TestMethod]
        public void TestIsInstanceOf()
        {
            Assert.IsInstanceOfType(new FileNotFoundException(), typeof (FileNotFoundException));
            Assert.IsInstanceOfType(new FileNotFoundException(), typeof (IOException));
            Assert.IsInstanceOfType(new FileNotFoundException(), typeof (Exception));
            Assert.IsInstanceOfType(new FileNotFoundException(), typeof (object));
            Assert.IsInstanceOfType(new FileNotFoundException(), typeof (Object));

            ExpectFail(() => Assert.IsInstanceOfType(new FileNotFoundException(), typeof (PathTooLongException)));
            ExpectFail(() => Assert.IsInstanceOfType(new IOException(), typeof (FileNotFoundException)));

            ExpectFail(() => Assert.IsInstanceOfType(null, typeof (FileNotFoundException)));
            ExpectFail(() => Assert.IsInstanceOfType(null, typeof (object)));

            ExpectFail(() => Assert.IsInstanceOfType(new IOException(), null));

            Assert.IsInstanceOfType((int) 5, typeof (int));
            Assert.IsInstanceOfType((int) 5, typeof (Int32));
            ExpectFail(() => Assert.IsInstanceOfType((int) 5, typeof (Int64)));

            Assert.IsInstanceOfType(new TestStruct(), typeof (TestStruct));
            Assert.IsInstanceOfType(new TestStruct(), typeof (ValueType));
        }

        [TestMethod]
        public void TestIsNotInstanceOf()
        {
            ExpectFail(() => Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (FileNotFoundException)));
            ExpectFail(() => Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (IOException)));
            ExpectFail(() => Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (Exception)));
            ExpectFail(() => Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (object)));
            ExpectFail(() => Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (Object)));

            Assert.IsNotInstanceOfType(new FileNotFoundException(), typeof (PathTooLongException));
            Assert.IsNotInstanceOfType(new IOException(), typeof (FileNotFoundException));

            Assert.IsNotInstanceOfType(null, typeof (FileNotFoundException));
            Assert.IsNotInstanceOfType(null, typeof (object));

            ExpectFail(() => Assert.IsNotInstanceOfType(new IOException(), null));

            ExpectFail(() => Assert.IsNotInstanceOfType((int) 5, typeof (int)));
            ExpectFail(() => Assert.IsNotInstanceOfType((int) 5, typeof (Int32)));
            Assert.IsNotInstanceOfType((int) 5, typeof (Int64));

            ExpectFail(() => Assert.IsNotInstanceOfType(new TestStruct(), typeof (TestStruct)));
            ExpectFail(() => Assert.IsNotInstanceOfType(new TestStruct(), typeof (ValueType)));
        }
    }
}