using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Competitions")]
    public class CompetitionsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CompetitionsMenu", "Competitions Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CompetitionsMenu);

            var newCompetitionButton = FindViewById<Button>(Resource.Id.NewCompetitionButton);
            newCompetitionButton.Click += delegate { StartActivity(typeof(CreateCompetitionForm)); };
        }
    }
}