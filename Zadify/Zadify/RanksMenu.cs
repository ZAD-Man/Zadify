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