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
    [Activity(Label = "Competitions")]
    public class CompetitionsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CompetitionsMenu);

            var newCompetitionButton = FindViewById<Button>(Resource.Id.NewCompetitionButton);
            newCompetitionButton.Click += delegate { StartActivity(typeof(CreateCompetitionForm)); };
        }
    }
}