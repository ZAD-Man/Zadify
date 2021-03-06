using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Custom Reward Details")]
    public class CustomRewardDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CustomRewardDetailsScreen", "Custom Reward Details Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CustomRewardDetailsScreen);

            var layout = FindViewById<LinearLayout>(Resource.Id.CustomRewardDetailsScreenLayout);
            layout.SetBackgroundResource(Resource.Color.darkblue);

            var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");

            var storedRewards = JavaIO.LoadData<List<Reward>>(this, "Rewards.zad");

            var position = Intent.GetIntExtra("Position", -1);

            if (storedGoals != null && storedRewards != null && position != -1)
            {
                var displayReward = storedRewards[position];

                displayReward.UpdateGoals(storedGoals);

                var customRewardDisplayTitle = FindViewById<TextView>(Resource.Id.CustomRewardDisplayTitle);
                customRewardDisplayTitle.Text = displayReward.Name;

                var customRewardDisplayContent = FindViewById<TextView>(Resource.Id.CustomRewardDisplayContent);
                if (displayReward.IsUnlocked())
                {
                    customRewardDisplayContent.Text = displayReward.Content;
                }
                else
                {
                    customRewardDisplayContent.Visibility = ViewStates.Gone;
                }

                var customRewardDisplayGoalList = FindViewById<ListView>(Resource.Id.CustomRewardDisplayGoalList);
                var requiredGoalStrings = displayReward.RequiredGoals.Select(requiredGoal => requiredGoal.Summary()).ToList();

                var rewardGoalListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, requiredGoalStrings);

                customRewardDisplayGoalList.Adapter = rewardGoalListAdapter;
            }
            else
            {
                Toast.MakeText(this, "Problem loading reward", ToastLength.Long).Show();
            }

            var deleteCustomRewardButton = FindViewById<Button>(Resource.Id.DeleteCustomRewardButton);
            deleteCustomRewardButton.Click += delegate
                {
                    var deleteCustomRewardForm = new Intent(this, typeof (DeleteCustomRewardForm));
                    deleteCustomRewardForm.PutExtra("Position", position);
                    StartActivity(deleteCustomRewardForm);
                    Finish();
                };
        }
    }
}