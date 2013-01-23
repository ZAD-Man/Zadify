using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal")]
    public class CreateGoalMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateGoalMenu", "Create Goal Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateGoalMenu);

            var createPredefinedGoalButton = FindViewById<Button>(Resource.Id.CreatePredefinedGoalButton);
            createPredefinedGoalButton.Click += delegate { StartActivity(typeof(CreatePredefinedGoalForm)); };

            var createCustomGoalButton = FindViewById<Button>(Resource.Id.CreateCustomGoalButton);
            createCustomGoalButton.Click += delegate { StartActivity(typeof(CreateCustomGoalForm)); };
            
            var createCompetitionButton = FindViewById<Button>(Resource.Id.CreateCompetitionButton);
            createCompetitionButton.Click += delegate { StartActivity(typeof(CreateCompetitionForm)); };
        }
    }
}