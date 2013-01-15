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
    [Activity(Label = "Update Goal")]
    public class UpdateGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("UpdateGoalForm", "Update Goal Form Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UpdateGoalForm);

            var updateGoalForm = FindViewById<TextView>(Resource.Id.UpdateGoalForm);
            //TODO: Change to a RelativeLayout, populate

            var updateGoalSubmitButton = FindViewById<Button>(Resource.Id.UpdateGoalSubmitButton);
            updateGoalSubmitButton.Click += delegate
                {
                    //TODO: Edit content and go back. Look into FinishActivity().
                };
        }
    }
}