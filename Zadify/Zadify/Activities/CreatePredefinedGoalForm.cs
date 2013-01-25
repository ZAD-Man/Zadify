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

        private DateTime _date = DateTime.Today;
        private Button _readingByDateSelectDate;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreatePredefinedGoalForm", "Create Predefined Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreatePredefinedGoalForm);

            var predefinedGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.PredefinedGoalTypeSpinner);
            var predefinedGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.predefinedGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            predefinedGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            predefinedGoalTypeSpinner.Adapter = predefinedGoalTypeAdapter;

            var readingGoalLayout = FindViewById<RelativeLayout>(Resource.Id.ReadingGoalLayout);

            var readingGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.ReadingGoalTypeSpinner);
            var readingGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            readingGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingGoalTypeSpinner.Adapter = readingGoalTypeAdapter;

            var readingByDateLayout = FindViewById<RelativeLayout>(Resource.Id.ReadingByDateLayout);

            var readingByDateNumber = FindViewById<EditText>(Resource.Id.ReadingByDateNumber);

            var readingByDateThingSpinner = FindViewById<Spinner>(Resource.Id.ReadingByDateThingSpinner);
            var readingByDateThingAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingThings, Android.Resource.Layout.SimpleSpinnerItem);
            readingByDateThingAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingByDateThingSpinner.Adapter = readingByDateThingAdapter;

            _readingByDateSelectDate = FindViewById<Button>(Resource.Id.ReadingByDateSelectDate);
            _readingByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitReadingByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitReadingByDateGoalButton);
            submitReadingByDateGoalButton.Click += delegate
                {
                    var goalNumber = int.Parse(readingByDateNumber.Text);
                    var items = ReadingItems.Books;
                    var selectedThings = readingByDateThingSpinner.GetItemAtPosition(readingByDateThingSpinner.SelectedItemPosition);
                    switch (selectedThings.ToString())
                    {
                        case "Book(s)":
                            items = ReadingItems.Books;
                            break;
                        case "Hour(s)":
                            items = ReadingItems.Hours;
                            break;
                        case "Minute(s)":
                            items = ReadingItems.Minutes;
                            break;
                        case "Word(s)":
                            items = ReadingItems.Words;
                            break;
                        case "Page(s)":
                            items = ReadingItems.Pages;
                            break;
                    }

                    var testReadingByDateGoal = new ReadingByDateGoal(_date, items, goalNumber);
                    JavaIO.SaveData(this, "TestGoal.txt", testReadingByDateGoal);
                        var testOutGoal = JavaIO.LoadData<ReadingByDateGoal>(this, "TestGoal.txt");
                    try
                    {
                        submitReadingByDateGoalButton.Text = testOutGoal.ReadingItems.ToString();
                    }
                    catch (Java.IO.FileNotFoundException e)
                    {
                        Log.Error("XMLTest:FileNotFound:", e.Message + e.StackTrace);
                    }
                    catch (Exception e)
                    {
                        Log.Error("XMLTest:General:", e.Message + e.StackTrace);
                        Log.Error("ReadingItems", testOutGoal.ReadingItems.ToString());
                        Log.Error("ItemsCompletedNr", testOutGoal.ItemsCompletedNumber.ToString());
                        Log.Error("DueDate", testOutGoal.DueDate.ToString());
                    }
                    //TODO: Go back to Goals Menu. Look into FinishActivity().
                };

            predefinedGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = predefinedGoalTypeSpinner.GetItemAtPosition(predefinedGoalTypeSpinner.SelectedItemPosition);
                    if (currentItem.ToString() == "Reading")
                    {
                        readingGoalLayout.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        readingGoalLayout.Visibility = ViewStates.Invisible;
                    }
                };

            readingGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = readingGoalTypeSpinner.GetItemAtPosition(readingGoalTypeSpinner.SelectedItemPosition);
                    if (currentItem.ToString() == "By Date")
                    {
                        readingByDateLayout.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        readingByDateLayout.Visibility = ViewStates.Gone;
                    }
                };
        }

        private void UpdateReadingByDateDate()
        {
            _readingByDateSelectDate.Text = _date.ToString("d");
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this._date = e.Date;
            UpdateReadingByDateDate();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, _date.Year, _date.Month - 1, _date.Day);
            }
            return null;
        }
    }
}