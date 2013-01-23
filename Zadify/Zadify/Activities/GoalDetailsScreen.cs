using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Goal Details")] //TODO: Replace with name of selected goal
    public class GoalDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("GoalDetailsScreen", "Goal Details Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.GoalDetailsScreen);

            var goalDetails = FindViewById<TextView>(Resource.Id.GoalDetails);
            //TODO: Change to a RelativeLayout

            var updateGoalButton = FindViewById<Button>(Resource.Id.UpdateGoalButton);
            updateGoalButton.Click += delegate { StartActivity(typeof(UpdateGoalForm)); };

            var deleteGoalButton = FindViewById<Button>(Resource.Id.DeleteGoalButton);
            deleteGoalButton.Click += delegate { StartActivity(typeof (DeleteGoalForm)); };
        }
    }
}