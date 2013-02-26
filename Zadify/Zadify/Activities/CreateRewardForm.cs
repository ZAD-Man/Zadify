using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Reward")]
    public class CreateRewardForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateRewardForm", "Create Reward Form created");

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