using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Ranks")]
    public class RanksMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("RanksMenu", "Ranks Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RanksMenu);

            var RanksList = FindViewById<ListView>(Resource.Id.RanksList);
            //TODO: Fill with buttons for each rank
        }
    }
}