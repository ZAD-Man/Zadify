using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify
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