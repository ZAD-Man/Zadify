using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Custom Rewards")]
    public class CustomRewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CustomRewardsMenu", "Custom Rewards Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CustomRewardsMenu);

            var createRewardButton = FindViewById<Button>(Resource.Id.CreateRewardButton);
            createRewardButton.Click += delegate { StartActivity(typeof (CreateRewardForm)); };

            var customRewardsList = FindViewById<ListView>(Resource.Id.CustomRewardsList);
        }
    }
}