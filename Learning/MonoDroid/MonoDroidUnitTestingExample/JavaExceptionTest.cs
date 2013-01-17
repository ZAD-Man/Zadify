using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonoDroidUnit
{
    [TestClass]
    public class JavaExceptionTest
    {
        [TestMethod]
        public void TestJavaException()
        {
            Java.Lang.JavaSystem.Load("non-existing-library.so");
        }
    }
}