using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace TestTests
{
    [TestClass]
    public class StringAssertTests : TestsBase
    {
        [TestMethod]
        public void TestContaints()
        {
            StringAssert.Contains("just a test", "t a t");
            StringAssert.Contains("just a test", "just");
            StringAssert.Contains("just a test", "test");

            ExpectFail(() => StringAssert.Contains("", "test"));
            StringAssert.Contains("just a test", "");
            ExpectFail(() => StringAssert.Contains(null, "test"));
            ExpectFail(() => StringAssert.Contains("just a test", null));

            ExpectFail(() => StringAssert.Contains("not an example", "t a t"));
            ExpectFail(() => StringAssert.Contains("not an example", "just"));
            ExpectFail(() => StringAssert.Contains("not an example", "test"));
        }

        [TestMethod]
        public void TestStartsWith()
        {
            StringAssert.StartsWith("just a test", "just");
            StringAssert.StartsWith("just a test", "");
            ExpectFail(() => StringAssert.StartsWith("just a test", null));
            ExpectFail(() => StringAssert.StartsWith("", "just"));
            ExpectFail(() => StringAssert.StartsWith(null, "just"));

            ExpectFail(() => StringAssert.StartsWith("just a test", "test"));
            ExpectFail(() => StringAssert.StartsWith("just a test", "ust"));
        }

        [TestMethod]
        public void TestEndsWith()
        {
            StringAssert.EndsWith("just a test", "test");
            StringAssert.EndsWith("just a test", "");
            ExpectFail(() => StringAssert.EndsWith("just a test", null));
            ExpectFail(() => StringAssert.EndsWith("", "test"));
            ExpectFail(() => StringAssert.EndsWith(null, "test"));

            ExpectFail(() => StringAssert.EndsWith("just a test", "just"));
            ExpectFail(() => StringAssert.EndsWith("just a test", "tes"));
        }

        [TestMethod]
        public void TestMatches()
        {
            StringAssert.Matches("hellooooo", new Regex("hell[o]+"));
            StringAssert.Matches("hellooooo world", new Regex("hell[o]+"));
            StringAssert.Matches("world hellooooo", new Regex("hell[o]+"));

            ExpectFail(() => StringAssert.Matches("helooooo", new Regex("hell[o]+")));

            StringAssert.Matches("hellooooo", new Regex("^hell[o]+$"));
            ExpectFail(() => StringAssert.Matches("hellooooo world", new Regex("^hell[o]+$")));
            ExpectFail(() => StringAssert.Matches("world hellooooo", new Regex("^hell[o]+$")));

            StringAssert.Matches("world hellooooo", new Regex(""));
            ExpectFail(() => StringAssert.Matches("world hellooooo", null));
            StringAssert.Matches("", new Regex(""));
            ExpectFail(() => StringAssert.Matches("", new Regex("hell[o]+")));
            ExpectFail(() => StringAssert.Matches(null, new Regex("hell[o]+")));
            ExpectFail(() => StringAssert.Matches(null, new Regex("")));
            ExpectFail(() => StringAssert.Matches("", null));
        }

        [TestMethod]
        public void TestDoesNotMatch()
        {
            ExpectFail(() => StringAssert.DoesNotMatch("hellooooo", new Regex("hell[o]+")));
            ExpectFail(() => StringAssert.DoesNotMatch("hellooooo world", new Regex("hell[o]+")));
            ExpectFail(() => StringAssert.DoesNotMatch("world hellooooo", new Regex("hell[o]+")));

            StringAssert.DoesNotMatch("helooooo", new Regex("hell[o]+"));

            ExpectFail(() => StringAssert.DoesNotMatch("hellooooo", new Regex("^hell[o]+$")));
            StringAssert.DoesNotMatch("hellooooo world", new Regex("^hell[o]+$"));
            StringAssert.DoesNotMatch("world hellooooo", new Regex("^hell[o]+$"));

            ExpectFail(() => StringAssert.DoesNotMatch("world hellooooo", new Regex("")));
            ExpectFail(() => StringAssert.DoesNotMatch("world hellooooo", null));
            ExpectFail(() => StringAssert.DoesNotMatch("", new Regex("")));
            StringAssert.DoesNotMatch("", new Regex("hell[o]+"));
            ExpectFail(() => StringAssert.DoesNotMatch(null, new Regex("hell[o]+")));
            ExpectFail(() => StringAssert.DoesNotMatch(null, new Regex("")));
            ExpectFail(() => StringAssert.DoesNotMatch("", null));
        }
    }
}