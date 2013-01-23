using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Update Goal")]
    public class UpdateGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("UpdateGoalForm", "Update Goal Form Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UpdateGoalForm);

            var updateGoalForm = FindViewById<TextView>(Resource.Id.UpdateGoalForm);
            //TODO: Change to a RelativeLayout, populate

            var updateGoalSubmitButton = FindViewById<Button>(Resource.Id.UpdateGoalSubmitButton);
            updateGoalSubmitButton.Click += delegate
                {
                    //TODO: Edit content and go back. Look into FinishActivity().
                };
        }
    }
}