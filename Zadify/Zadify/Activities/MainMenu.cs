using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using ServiceStack.ServiceClient.Web;

namespace Zadify.Activities
{
    [Activity(Label = "Zadify", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("MainMenu", "Main Menu Created");
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainMenu);

            try
            {
                var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (goalsList == null)
                {
                    goalsList = new List<Goal>();
                    JavaIO.SaveData(this, "Goals.zad", goalsList);
                }
            }
            catch (Exception)
            {
                var goalsList = new List<Goal>();
                JavaIO.SaveData(this, "Goals.zad", goalsList);
            }

            var goalsButton = FindViewById<Button>(Resource.Id.GoalsButton);
            goalsButton.Click += delegate { StartActivity(typeof (GoalsMenu)); };

            var rewardsButton = FindViewById<Button>(Resource.Id.RewardsButton);
            rewardsButton.Click += delegate { StartActivity(typeof (RewardsMenu)); };

            var socialButton = FindViewById<Button>(Resource.Id.SocialButton);
            socialButton.Click += delegate { StartActivity(typeof (SocialMenu)); };

            var settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            settingsButton.Click += delegate { StartActivity(typeof (SettingsMenu)); };

            try
            {
                var client = new JsonServiceClient("http://localhost:62577/servicestack");
                var response = client.Get<HelloResponse>("~/hello/Zach!");

                Toast.MakeText(this, response.Result, ToastLength.Long);
                Log.Error("REST rsult", response.Result);
            }
            catch (Exception e)
            {
                Log.Error("ReST Error", e.Message + e.StackTrace);
            }
        }
    }
}