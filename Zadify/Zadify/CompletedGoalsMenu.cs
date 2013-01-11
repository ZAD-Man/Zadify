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
    [Activity(Label = "Completed Goals")]
    public class CompletedGoalsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CompletedGoalsMenu);

            var completedGoalsList = FindViewById<ListView>(Resource.Id.CompletedGoalsList);
            //TODO: Presumably add a button for each goal using .AddView()
        }
    }
}