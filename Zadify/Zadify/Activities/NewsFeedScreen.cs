using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Newsfeed")]
    public class NewsFeedScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("NewsFeedScreen", "News Feed Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.NewsFeedScreen);

            var newsFeedButton = FindViewById<Button>(Resource.Id.NewsFeedButton);
            //TODO: Fill with friends' activities
        }
    }
}