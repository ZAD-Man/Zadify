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

            var layout = FindViewById<LinearLayout>(Resource.Id.RewardsMenuLayout);
            layout.SetBackgroundResource(Resource.Color.darkblue);

            var RanksButton = FindViewById<Button>(Resource.Id.RanksButton);
            RanksButton.Click += delegate { StartActivity(typeof (RanksMenu)); };

            var UnlockedItemsButton = FindViewById<Button>(Resource.Id.UnlockedItemsButton);
            UnlockedItemsButton.Click += delegate { StartActivity(typeof (UnlockedItemsScreen)); };

            var customRewardsButton = FindViewById<Button>(Resource.Id.CustomRewardsButton);
            customRewardsButton.Click += delegate { StartActivity(typeof (CustomRewardsMenu)); };
        }
    }
}