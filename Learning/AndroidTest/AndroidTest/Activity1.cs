using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidTest.com.parasoft.soatest;

namespace AndroidTest
{
    [Activity(Label = "AndroidTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var aButton = FindViewById<Button>(Resource.Id.aButton);
            var aLabel = FindViewById<TextView>(Resource.Id.helloLabel);

            aButton.Click += (sender, e) =>
                {
                    try
                    {
                        var webServiceCalc = new Calculator();
                        aLabel.Text = webServiceCalc.add(23, 42).ToString();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("WebService", ex.Message + ex.StackTrace);
                    }
                };
        }
    }
}