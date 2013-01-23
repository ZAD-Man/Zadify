using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal")]
    public class CreateCustomGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateCustomeGoalForm", "Create Custom Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateCustomGoalForm);

            var customGoalForm = FindViewById<TextView>(Resource.Id.CustomGoalForm);
            //TODO: Change to a RelativeLayout

            var submitCustomGoalButton = FindViewById<Button>(Resource.Id.SubmitCustomGoalButton);
            submitCustomGoalButton.Click += delegate
            {
                //TODO: Go back to Goals Menu. Look into FinishActivity().
            };
        }
    }
}