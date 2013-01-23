using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Unlocked Items")]
    public class UnlockedItemsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("UnlockedItemsScreen", "Unlocked Items Screen created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UnlockedItemsScreen);

            var UnlockedItemsList = FindViewById<ListView>(Resource.Id.UnlockedItemsList);
            //TODO: Fill with buttons for each rank
        }
    }
}