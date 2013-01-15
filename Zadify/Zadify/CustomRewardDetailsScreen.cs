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
    [Activity(Label = "Custom Reward Details")] //TODO: Replace with name of selected goal, or similar
    public class CustomRewardDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CustomRewardDetailsScreen", "Custom Reward Details Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CustomRewardDetailsScreen);

            var customRewardDetails = FindViewById<TextView>(Resource.Id.CustomRewardDetails);
            //TODO: Change to a RelativeLayout

            var deleteCustomRewardButton = FindViewById<Button>(Resource.Id.DeleteCustomRewardButton);
            deleteCustomRewardButton.Click += delegate
                {
                    //TODO: Make pop-up confirmation dialog
                };
        }
    }
}