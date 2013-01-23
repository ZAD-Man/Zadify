using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal")]
    public class CreatePredefinedGoalForm : Activity
    {
        private const int DATE_DIALOG_ID = 0;

        private DateTime date;
        private Button readingByDateSelectDate;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreatePredefinedGoalForm", "Create Predefined Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreatePredefinedGoalForm);
            
            var predefinedGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.PredefinedGoalTypeSpinner);
            var predefinedGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.predefinedGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            predefinedGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            predefinedGoalTypeSpinner.Adapter = predefinedGoalTypeAdapter;

            var readingGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.ReadingGoalTypeSpinner);
            readingGoalTypeSpinner.Visibility = ViewStates.Gone;
            var readingGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            readingGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingGoalTypeSpinner.Adapter = readingGoalTypeAdapter;

            var readingByDateText1 = FindViewById<TextView>(Resource.Id.ReadingByDateText1);
            readingByDateText1.Visibility = ViewStates.Gone;

            var readingByDateNumberSpinner = FindViewById<Spinner>(Resource.Id.ReadingByDateNumberSpinner);
            readingByDateNumberSpinner.Visibility = ViewStates.Gone;
            var readingByDateNumberAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.numbers, Android.Resource.Layout.SimpleSpinnerItem);
            readingByDateNumberAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingByDateNumberSpinner.Adapter = readingByDateNumberAdapter;

            var readingByDateThingSpinner = FindViewById<Spinner>(Resource.Id.ReadingByDateThingSpinner);
            readingByDateThingSpinner.Visibility = ViewStates.Gone;
            var readingByDateThingAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingThings, Android.Resource.Layout.SimpleSpinnerItem);
            readingByDateThingAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingByDateThingSpinner.Adapter = readingByDateThingAdapter;

            var readingByDateText2 = FindViewById<TextView>(Resource.Id.ReadingByDateText2);
            readingByDateText2.Visibility = ViewStates.Gone;

            readingByDateSelectDate = FindViewById<Button>(Resource.Id.ReadingByDateSelectDate);
            readingByDateSelectDate.Visibility = ViewStates.Gone;
            readingByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitPredefinedGoalButton = FindViewById<Button>(Resource.Id.SubmitReadingByDateGoalButton);
            submitPredefinedGoalButton.Click += delegate
                {
                    //TODO: Go back to Goals Menu. Look into FinishActivity().
                };

            predefinedGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = predefinedGoalTypeSpinner.GetItemAtPosition(predefinedGoalTypeSpinner.SelectedItemPosition);
                    if (currentItem.ToString() == "Reading")
                    {
                        readingGoalTypeSpinner.Visibility = ViewStates.Visible;
                    } 
                    else 
                    {
                        readingGoalTypeSpinner.Visibility = ViewStates.Gone;
                    }
                };

            readingGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = readingGoalTypeSpinner.GetItemAtPosition(readingGoalTypeSpinner.SelectedItemPosition);
                    if (currentItem.ToString() == "By Date")
                    {
                        readingByDateSelectDate.Visibility = ViewStates.Visible;
                        readingByDateText1.Visibility = ViewStates.Visible;
                        readingByDateNumberSpinner.Visibility = ViewStates.Visible;
                        readingByDateThingSpinner.Visibility = ViewStates.Visible;
                        readingByDateText2.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        readingByDateSelectDate.Visibility = ViewStates.Gone;
                        readingByDateText1.Visibility = ViewStates.Gone;
                        readingByDateNumberSpinner.Visibility = ViewStates.Gone;
                        readingByDateThingSpinner.Visibility = ViewStates.Gone;
                        readingByDateText2.Visibility = ViewStates.Gone;
                    }
                };
        }
        
        private void UpdateReadingByDateDate()
        {
            readingByDateSelectDate.Text = date.ToString("d");
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.date = e.Date;
            UpdateReadingByDateDate();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, date.Year, date.Month - 1, date.Day);
            }
            return null;
        }
    }
}