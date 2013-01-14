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
    [Activity(Label = "Rewards")]
    public class RewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RewardsMenu);

            var zadifyRewardsButton = FindViewById<Button>(Resource.Id.ZadifyRewardsButton);
            zadifyRewardsButton.Click += delegate { StartActivity(typeof(ZadifyRewardsMenu)); };

            var customRewardsButton = FindViewById<Button>(Resource.Id.CustomRewardsButton);
            customRewardsButton.Click += delegate { StartActivity(typeof(CustomRewardsMenu)); };
        }
    }
}