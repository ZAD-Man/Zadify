using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Zadify
{
    [Activity(Label = "Zadify", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainMenu);

            var goalsButton = FindViewById<Button>(Resource.Id.GoalsButton);
            goalsButton.Click += delegate
                {
                    StartActivity(typeof(GoalsMenu));
                };

            var rewardsButton = FindViewById<Button>(Resource.Id.RewardsButton);
            rewardsButton.Click += delegate
            {
                StartActivity(typeof(RewardsMenu));
            };

            var socialButton = FindViewById<Button>(Resource.Id.SocialButton);
            socialButton.Click += delegate
            {
                StartActivity(typeof(SocialMenu));
            };

            var settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            settingsButton.Click += delegate
            {
                StartActivity(typeof(SettingsMenu));
            };

        }
    }
}

