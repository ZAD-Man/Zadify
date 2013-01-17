using System.Reflection;
using Android.App;
using MonoDroidUnitTesting;

namespace ZadifyTests
{
    [Activity(Label = "ZadifyTests", MainLauncher = true, Icon = "@drawable/icon")]
    public class TestActivity : GuiTestRunnerActivity
    {
        protected override TestRunner CreateTestRunner()
        {
            var testRunner = new TestRunner();

            testRunner.AddTests(Assembly.GetExecutingAssembly());

            return testRunner;
        }
    }
}