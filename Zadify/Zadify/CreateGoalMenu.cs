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
    [Activity(Label = "Create Goal")]
    public class CreateGoalMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateGoalMenu);

            var createPredefinedGoalButton = FindViewById<Button>(Resource.Id.CreatePredefinedGoalButton);
            createPredefinedGoalButton.Click += delegate { StartActivity(typeof(CreatePredefinedGoalForm)); };

            var createCustomGoalButton = FindViewById<Button>(Resource.Id.CreateCustomGoalButton);
            createCustomGoalButton.Click += delegate { StartActivity(typeof(CreateCustomGoalForm)); };
            
            var createCompetitionButton = FindViewById<Button>(Resource.Id.CreateCompetitionButton);
            createCompetitionButton.Click += delegate { StartActivity(typeof(CreateCompetitionForm)); };
        }
    }
}