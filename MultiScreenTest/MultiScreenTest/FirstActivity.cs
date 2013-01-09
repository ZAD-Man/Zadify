using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiScreenTest
{
    [Activity(Label = "MultiScreenTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class FirstActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Use UI created in Main.axml
            SetContentView(Resource.Layout.Main);

            var showSecond = FindViewById<Button>(Resource.Id.showSecond);
            showSecond.Click += (sender, e) =>
            {
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("FirstData", "Data from FirstActivity (ZAD-Man ROX!!!)");
                StartActivity(second);
            };
        }
    }
}

