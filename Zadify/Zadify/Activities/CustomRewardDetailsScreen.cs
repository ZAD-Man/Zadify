using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
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