using System;
using System.Net;
using System.Net.NetworkInformation;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidTest.com.parasoft.soatest;
using AndroidTest.ZWebServiceTutorial;

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
            var zTutorialButton = FindViewById<Button>(Resource.Id.zTutorialButton);
            var aLabel = FindViewById<TextView>(Resource.Id.helloLabel);

            aButton.Click += (sender, e) =>
                {
                    try
                    {
                        var webServiceCalc = new Calculator();
                        Ping ping = new Ping();
                        var pingReply = ping.Send("192.55.86.58");
                        Toast.MakeText(this, pingReply.Status.ToString(), ToastLength.Long).Show();
                        aLabel.Text = webServiceCalc.add(23, 42).ToString();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("WebService", ex.Message + ex.StackTrace);
                    }
                };

            zTutorialButton.Click += delegate
                {
                    try
                    {
                        var zService1 = new Service1();
                        var greeting = zService1.WebMethodTheFirst("Zach", "Durtschi");
                        aLabel.Text = greeting;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("ZWebService", ex.Message + ex.StackTrace);
                    }
                };
        }
    }
}