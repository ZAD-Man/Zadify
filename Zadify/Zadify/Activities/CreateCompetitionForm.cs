using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "My Activity")]
    public class CreateCompetitionForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateCompetitionForm", "Create Competition Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateCompetitionForm);

            var competitionForm = FindViewById<TextView>(Resource.Id.CompetitionForm);
            //TODO: Change to a RelativeLayout

            var submitCompetitionButton = FindViewById<Button>(Resource.Id.SubmitCompetitionButton);
            submitCompetitionButton.Click += delegate
            {
                //TODO: Go back to Goals Menu. Look into FinishActivity().
            };
        }
    }
}