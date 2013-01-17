using System.Reflection;
using Android.App;

namespace MonoDroidUnitTesting
{
    /// <summary>
    /// Example application for running some unit tests.
    /// </summary>
    [Activity(Label = "MonoDroidUnitTesting Example", MainLauncher = true, Icon = "@drawable/icon")]
    public class TestActivity : GuiTestRunnerActivity
    {
        protected override TestRunner CreateTestRunner()
        {
            TestRunner runner = new TestRunner();
            // Run all tests from this assembly
            runner.AddTests(Assembly.GetExecutingAssembly());
            return runner;
        }
    }
}