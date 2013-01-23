using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("Settings", "Settings Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SettingsMenu);

            var settings = FindViewById<TextView>(Resource.Id.Settings);
        }
    }
}