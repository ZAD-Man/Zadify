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

namespace Zadify
{
    [Activity(Label = "Custom Rewards")]
    public class CustomRewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CustomRewardsMenu);

            var createRewardButton = FindViewById<Button>(Resource.Id.CreateRewardButton);
            createRewardButton.Click += delegate { StartActivity(typeof(CreateRewardForm)); };

            var customRewardsList = FindViewById<ListView>(Resource.Id.CustomRewardsList);
        }
    }
}