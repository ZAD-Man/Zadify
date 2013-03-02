using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Reward")]
    public class CreateRewardForm : Activity
    {
        private List<Goal> _goalsList = new List<Goal>();

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateRewardForm", "Create Reward Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateRewardForm);

            var customRewardTitle = FindViewById<EditText>(Resource.Id.CustomRewardTitle);

            var customRewardContent = FindViewById<EditText>(Resource.Id.CustomRewardContent);

            var customRewardAddGoalButton = FindViewById<Button>(Resource.Id.CustomRewardAddGoalButton);
            customRewardAddGoalButton.Click += delegate { StartActivity(typeof (SelectCustomRewardGoal)); };

            var submitCustomRewardButton = FindViewById<Button>(Resource.Id.SubmitCustomRewardButton);
            submitCustomRewardButton.Click += delegate
                {
                    var title = customRewardTitle.Text;
                    var content = customRewardContent.Text;
                    if (_goalsList.Count != 0)
                    {
                        var reward = new Reward(title, content, _goalsList);
                        var storedRewards = JavaIO.LoadData<List<Reward>>(this, "Rewards.zad");
                        storedRewards.Add(reward);
                        var successfulSave = JavaIO.SaveData(this, "Rewards.zad", storedRewards);
                        if (successfulSave)
                        {
                            Toast.MakeText(this, "Reward Saved", ToastLength.Long).Show();
                            Finish();
                        }
                        else
                        {
                            Log.Error("CreateRewardForm", "Save reward error");
                            Toast.MakeText(this, "Error saving reward", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Must select a goal", ToastLength.Long).Show();
                    }
                };
        }

        protected override void OnRestart()
        {
            Log.Info("CreateRewardForm", "Create Reward Form restarted");

            base.OnRestart();

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);

            var position = preferences.GetInt("SelectedGoalPosition", -1);

            if (position != -1)
            {
                var storedGoalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                var sortedGoals = storedGoalsList.Where(goal => !goal.IsPastDue()).ToList();
                var newGoal = sortedGoals[position];
                _goalsList.Add(newGoal);
            }
            var rewardGoalsStrings = _goalsList.Select(goal => goal.Summary()).ToList();

            var customRewardGoalsList = FindViewById<ListView>(Resource.Id.CustomRewardGoalsList);
            var customRewardGoalsListAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, rewardGoalsStrings);
            customRewardGoalsList.Adapter = customRewardGoalsListAdapter;
        }
    }
}