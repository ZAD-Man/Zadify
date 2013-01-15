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
    [Activity(Label = "Create Goal")]
    public class CreatePredefinedGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreatePredefinedGoalForm", "Create Predefined Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreatePredefinedGoalForm);

            var predefinedGoalForm = FindViewById<TextView>(Resource.Id.PredefinedGoalForm);
                //TODO: Change to a RelativeLayout

            var submitPredefinedGoalButton = FindViewById<Button>(Resource.Id.SubmitPredefinedGoalButton);
            submitPredefinedGoalButton.Click += delegate
                {
                    //TODO: Go back to Goals Menu. Look into FinishActivity().
                };
        }
    }
}