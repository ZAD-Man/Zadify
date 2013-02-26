using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Rewards")]
    public class RewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("RewardsMenu", "Rewards Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RewardsMenu);

            var zadifyRewardsButton = FindViewById<Button>(Resource.Id.ZadifyRewardsButton);
            zadifyRewardsButton.Click += delegate { StartActivity(typeof (ZadifyRewardsMenu)); };

            var customRewardsButton = FindViewById<Button>(Resource.Id.CustomRewardsButton);
            customRewardsButton.Click += delegate { StartActivity(typeof (CustomRewardsMenu)); };
        }
    }
}