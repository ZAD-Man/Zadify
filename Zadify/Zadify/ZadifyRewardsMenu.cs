using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify
{
    [Activity(Label = "Zadify Rewards")]
    public class ZadifyRewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("ZadifyRewardsMenu", "Zadify Rewards Menu Created");
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ZadifyRewardsMenu);

            var RanksButton = FindViewById<Button>(Resource.Id.RanksButton);
            RanksButton.Click += delegate { StartActivity(typeof(RanksMenu)); };

            var UnlockedItemsButton = FindViewById<Button>(Resource.Id.UnlockedItemsButton);
            UnlockedItemsButton.Click += delegate { StartActivity(typeof(UnlockedItemsScreen)); };
        }
    }
}