using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Delete Goal")]
    public class DeleteGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("Delete Goal Form", "Delete Goal Form Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DeleteGoalForm);

            var confirmDeleteButton = FindViewById<Button>(Resource.Id.ConfirmDeleteGoalButton);
            //TODO: Make this into a pop-up confirmation instead
        }
    }
}