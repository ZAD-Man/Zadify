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