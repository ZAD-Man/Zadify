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
    public class CreateCustomGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateCustomeGoalForm", "Create Custom Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateCustomGoalForm);

            var customGoalForm = FindViewById<TextView>(Resource.Id.CustomGoalForm);
            //TODO: Change to a RelativeLayout

            var submitCustomGoalButton = FindViewById<Button>(Resource.Id.SubmitCustomGoalButton);
            submitCustomGoalButton.Click += delegate
            {
                //TODO: Go back to Goals Menu. Look into FinishActivity().
            };
        }
    }
}