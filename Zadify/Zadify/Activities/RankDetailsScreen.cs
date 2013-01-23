using Android.App;
using Android.OS;
using Android.Util;

namespace Zadify.Activities
{
    [Activity(Label = "Rank Details")] //TODO: Replace with name of selected rank?
    public class RankDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("RanksDetailsScreen", "Ranks Details Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RankDetailsScreen);
        }
    }
}