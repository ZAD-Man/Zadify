using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTests
{
    public class TestsBase
    {
        protected static void ExpectFail(Action failFunc)
        {
            bool failed = false;
            try
            {
                failFunc();
            }
            catch (AssertFailedException)
            {
                failed = true;
            }

            if (!failed)
            {
                Assert.Fail();
            }
        }
    }
}