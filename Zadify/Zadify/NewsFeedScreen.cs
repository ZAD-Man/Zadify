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
    [Activity(Label = "Newsfeed")]
    public class NewsFeedScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.NewsFeedScreen);

            var newsFeedButton = FindViewById<Button>(Resource.Id.NewsFeedButton);
            //TODO: Fill with friends' activities
        }
    }
}