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
    [Activity(Label = "My Activity")]
    public class CreateCompetitionForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateCompetitionForm);

            var competitionForm = FindViewById<TextView>(Resource.Id.CompetitionForm);
            //TODO: Change to a RelativeLayout

            var submitCompetitionButton = FindViewById<Button>(Resource.Id.SubmitCompetitionButton);
            submitCompetitionButton.Click += delegate
            {
                //TODO: Go back to Goals Menu. Look into FinishActivity().
            };
        }
    }
}