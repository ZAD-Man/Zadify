using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Social")]
    public class SocialMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("SocialMenu", "Social Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SocialMenu);

            var newsFeedButton = FindViewById<Button>(Resource.Id.NewsFeedButton);
            newsFeedButton.Click += delegate { StartActivity(typeof(NewsFeedScreen)); };

            var competitionsButton = FindViewById<Button>(Resource.Id.CompetitionsButton);
            competitionsButton.Click += delegate { StartActivity(typeof(CompetitionsMenu)); };
        }
    }
}