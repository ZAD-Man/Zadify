using System.Collections.Generic;
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

            var customRewardTitle = FindViewById<EditText>(Resource.Id.CustomRewardTitle);

            var customRewardContent = FindViewById<EditText>(Resource.Id.CustomRewardContent);

            var customRewardsGoalsList = FindViewById<ListView>(Resource.Id.CustomRewardGoalsList);
            var goalsList = new List<Goal>(); //TODO: Fill this list whenever a new goal is added

            var submitCustomRewardButton = FindViewById<Button>(Resource.Id.SubmitCustomRewardButton);
            submitCustomRewardButton.Click += delegate
                {
                    var title = customRewardTitle.Text;
                    var content = customRewardContent.Text;
                    if (goalsList.Count != 0)
                    {
                        var reward = new Reward(title, content, goalsList);
                        //TODO: Save reward in Reward.zad
                    }
                    else
                    {
                        Toast.MakeText(this, "Must select a goal", ToastLength.Long).Show();
                    }
                };
        }
    }
}