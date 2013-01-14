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
    [Activity(Label = "Create Reward")]
    public class CreateRewardForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateRewardForm);

            var customRewardForm = FindViewById<TextView>(Resource.Id.CustomRewardForm);
            //TODO: Change to a RelativeLayout

            var submitCustomRewardButton = FindViewById<Button>(Resource.Id.SubmitCustomRewardButton);
            submitCustomRewardButton.Click += delegate
            {
                //TODO: Go back to Custom Rewards Menu. Look into FinishActivity().
            };
        }
    }
}