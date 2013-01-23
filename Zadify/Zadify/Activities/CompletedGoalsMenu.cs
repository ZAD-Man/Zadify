using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Completed Goals")]
    public class CompletedGoalsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CompletedGoalsMenu", "Completed Goals Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CompletedGoalsMenu);

            var completedGoalsList = FindViewById<ListView>(Resource.Id.CompletedGoalsList);
            //TODO: Presumably add a button for each goal using .AddView()
        }
    }
}