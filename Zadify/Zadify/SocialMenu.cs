using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zadify
{
    [Activity(Label = "Social")]
    public class SocialMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SocialMenu);

            var newsFeedButton = FindViewById<Button>(Resource.Id.NewsFeedButton);
            newsFeedButton.Click += delegate { StartActivity(typeof(NewsFeedScreen)); };

            var competitionsButton = FindViewById<Button>(Resource.Id.CompetitionsButton);
            competitionsButton.Click += delegate { StartActivity(typeof(CompetitionsMenu)); };
        }
    }
}