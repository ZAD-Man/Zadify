using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class CreatePredefinedGoalForm : Activity
    {
        private const int DATE_DIALOG_ID = 0;

        private DateTime _goalDate = DateTime.Today;
        private Button _readingByDateSelectDate;
        private Button _readingPerTimespanSelectDate;
        private Button _fitnessByDateSelectDate;
        private Button _fitnessPerTimespanSelectDate;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreatePredefinedGoalForm", "Create Predefined Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreatePredefinedGoalForm);

            var predefinedGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.PredefinedGoalTypeSpinner);
            var predefinedGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.predefinedGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            predefinedGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            predefinedGoalTypeSpinner.Adapter = predefinedGoalTypeAdapter;

            #region Fitness Goals

            var fitnessGoalLayout = FindViewById<RelativeLayout>(Resource.Id.FitnessGoalLayout);

            var fitnessGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.FitnessGoalTypeSpinner);
            var fitnessGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.fitnessGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            fitnessGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fitnessGoalTypeSpinner.Adapter = fitnessGoalTypeAdapter;

            #region Fitness By Date

            var fitnessByDateLayout = FindViewById<RelativeLayout>(Resource.Id.FitnessByDateLayout);

            var fitnessByDateNumber = FindViewById<EditText>(Resource.Id.FitnessByDateNumber);

            var fitnessByDateItemsSpinner = FindViewById<Spinner>(Resource.Id.FitnessByDateItemsSpinner);
            var fitnessByDateItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.fitnessActivities, Android.Resource.Layout.SimpleSpinnerItem);
            fitnessByDateItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fitnessByDateItemsSpinner.Adapter = fitnessByDateItemsAdapter;

            _fitnessByDateSelectDate = FindViewById<Button>(Resource.Id.FitnessByDateSelectDate);
            _fitnessByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFitnessByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitFitnessByDateGoalButton);
            submitFitnessByDateGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) >= 0)
                    {
                        var goalNumber = int.Parse(fitnessByDateNumber.Text);
                        var items = FitnessItems.Pushups;
                        var selectedItems = fitnessByDateItemsSpinner.GetItemAtPosition(fitnessByDateItemsSpinner.SelectedItemPosition);
                        switch (selectedItems.ToString())
                        {
                            case "Pushup(s)":
                                items = FitnessItems.Pushups;
                                break;
                            case "Pullup(s)":
                                items = FitnessItems.Pullups;
                                break;
                            case "Situp(s)":
                                items = FitnessItems.Situps;
                                break;
                            case "Mile(s) Ran":
                                items = FitnessItems.MilesRun;
                                break;
                            case "Kilometer(s) Ran":
                                items = FitnessItems.KilometersRun;
                                break;
                        }

                        try
                        {
                            var fitnessByDateGoal = new FitnessGoal(_goalDate, goalNumber, items);
                            var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                            goalsList.Add(fitnessByDateGoal);
                            bool successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                            }
                        }
                        catch (Exception e)
                        {
                            Toast.MakeText(this, "Error: " + e.Message, ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date in past", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Fitness Per Timespan

            var fitnessPerTimespanLayout = FindViewById<RelativeLayout>(Resource.Id.FitnessPerTimespanLayout);

            var fitnessPerTimespanNumber = FindViewById<EditText>(Resource.Id.FitnessPerTimespanNumber);

            var fitnessPerTimespanItemsSpinner = FindViewById<Spinner>(Resource.Id.FitnessPerTimespanItemsSpinner);
            var fitnessPerTimespanItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.fitnessActivities, Android.Resource.Layout.SimpleSpinnerItem);
            fitnessPerTimespanItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fitnessPerTimespanItemsSpinner.Adapter = fitnessPerTimespanItemsAdapter;

            var fitnessPerTimespanTimespanSpinner = FindViewById<Spinner>(Resource.Id.FitnessPerTimespanTimespanSpinner);
            var fitnessPerTimespanTimespanAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.repeatingTimespans, Android.Resource.Layout.SimpleSpinnerItem);
            fitnessPerTimespanTimespanAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fitnessPerTimespanTimespanSpinner.Adapter = fitnessPerTimespanTimespanAdapter;

            _fitnessPerTimespanSelectDate = FindViewById<Button>(Resource.Id.FitnessPerTimespanSelectDate);
            _fitnessPerTimespanSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFitnessPerTimespanGoalButton = FindViewById<Button>(Resource.Id.SubmitFitnessPerTimespanGoalButton);
            submitFitnessPerTimespanGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) >= 0)
                    {
                        var goalNumber = int.Parse(fitnessPerTimespanNumber.Text);
                        var items = FitnessItems.Pushups;
                        var timespan = new TimeSpan();
                        var selectedItems = fitnessPerTimespanItemsSpinner.GetItemAtPosition(fitnessPerTimespanItemsSpinner.SelectedItemPosition);
                        var selectedTimespan = fitnessPerTimespanTimespanSpinner.GetItemAtPosition(fitnessPerTimespanTimespanSpinner.SelectedItemPosition);
                        switch (selectedItems.ToString())
                        {
                            case "Pushup(s)":
                                items = FitnessItems.Pushups;
                                break;
                            case "Pullup(s)":
                                items = FitnessItems.Pullups;
                                break;
                            case "Situp(s)":
                                items = FitnessItems.Situps;
                                break;
                            case "Mile(s) Ran":
                                items = FitnessItems.MilesRun;
                                break;
                            case "Kilometer(s) Ran":
                                items = FitnessItems.KilometersRun;
                                break;
                        }

                        switch (selectedTimespan.ToString())
                        {
                            case "Day":
                                timespan = new TimeSpan(1, 0, 0, 0);
                                break;
                            case "Week":
                                timespan = new TimeSpan(7, 0, 0, 0);
                                break;
                        }

                        try
                        {
                            var fitnessPerTimespanGoal = new FitnessGoal(_goalDate, goalNumber, items, timespan);
                            var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                            goalsList.Add(fitnessPerTimespanGoal);
                            bool successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                            }
                        }
                        catch (Exception e)
                        {
                            Toast.MakeText(this, "Error: " + e.Message, ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date in past", ToastLength.Long).Show();
                    }
                };

            #endregion

            #endregion

            #region Reading Goals

            var readingGoalLayout = FindViewById<RelativeLayout>(Resource.Id.ReadingGoalLayout);

            var readingGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.ReadingGoalTypeSpinner);
            var readingGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            readingGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingGoalTypeSpinner.Adapter = readingGoalTypeAdapter;

            #region Reading By Date

            var readingByDateLayout = FindViewById<RelativeLayout>(Resource.Id.ReadingByDateLayout);

            var readingByDateNumber = FindViewById<EditText>(Resource.Id.ReadingByDateNumber);

            var readingByDateItemsSpinner = FindViewById<Spinner>(Resource.Id.ReadingByDateItemsSpinner);
            var readingByDateItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingItems, Android.Resource.Layout.SimpleSpinnerItem);
            readingByDateItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingByDateItemsSpinner.Adapter = readingByDateItemsAdapter;

            _readingByDateSelectDate = FindViewById<Button>(Resource.Id.ReadingByDateSelectDate);
            _readingByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitReadingByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitReadingByDateGoalButton);
            submitReadingByDateGoalButton.Click += delegate
                {
                    var goalNumber = int.Parse(readingByDateNumber.Text);
                    var items = ReadingItems.Books;
                    var selectedItems = readingByDateItemsSpinner.GetItemAtPosition(readingByDateItemsSpinner.SelectedItemPosition);
                    switch (selectedItems.ToString())
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
                    if (_goalDate.CompareTo(DateTime.Today) >= 0)
                    {
                        try
                        {
                            var readingByDateGoal = new ReadingGoal(_goalDate, goalNumber, items);
                            var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                            goalsList.Add(readingByDateGoal);
                            bool successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Error saving goal", ToastLength.Long).Show();
                            }
                        }
                        catch (Exception e)
                        {
                            Toast.MakeText(this, "Error: " + e.Message, ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date in past", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Reading Per Timespan

            var readingPerTimespanLayout = FindViewById<RelativeLayout>(Resource.Id.ReadingPerTimespanLayout);

            var readingPerTimespanNumber = FindViewById<EditText>(Resource.Id.ReadingPerTimespanNumber);

            var readingPerTimespanItemsSpinner = FindViewById<Spinner>(Resource.Id.ReadingPerTimespanItemsSpinner);
            var readingPerTimespanItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.readingItems, Android.Resource.Layout.SimpleSpinnerItem);
            readingPerTimespanItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingPerTimespanItemsSpinner.Adapter = readingPerTimespanItemsAdapter;

            var readingPerTimespanTimespanSpinner = FindViewById<Spinner>(Resource.Id.ReadingPerTimespanTimespanSpinner);
            var readingPerTimespanTimespanAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.repeatingTimespans, Android.Resource.Layout.SimpleSpinnerItem);
            readingPerTimespanTimespanAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            readingPerTimespanTimespanSpinner.Adapter = readingPerTimespanTimespanAdapter;

            _readingPerTimespanSelectDate = FindViewById<Button>(Resource.Id.ReadingPerTimespanSelectDate);
            _readingPerTimespanSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitReadingPerTimespanGoalButton = FindViewById<Button>(Resource.Id.SubmitReadingPerTimespanGoalButton);
            submitReadingPerTimespanGoalButton.Click += delegate
            {
                if (_goalDate.CompareTo(DateTime.Today) >= 0)
                {
                    var goalNumber = int.Parse(readingPerTimespanNumber.Text);
                    var items = ReadingItems.Books;
                    var timespan = new TimeSpan();
                    var selectedItems = readingPerTimespanItemsSpinner.GetItemAtPosition(readingPerTimespanItemsSpinner.SelectedItemPosition);
                    var selectedTimespan = readingPerTimespanTimespanSpinner.GetItemAtPosition(readingPerTimespanTimespanSpinner.SelectedItemPosition);
                    switch (selectedItems.ToString())
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
                        case "Page(s)":
                            items = ReadingItems.Pages;
                            break;
                        case "Word(s)":
                            items = ReadingItems.Words;
                            break;
                    }

                    switch (selectedTimespan.ToString())
                    {
                        case "Day":
                            timespan = new TimeSpan(1, 0, 0, 0);
                            break;
                        case "Week":
                            timespan = new TimeSpan(7, 0, 0, 0);
                            break;
                    }

                    try
                    {
                        var readingPerTimespanGoal = new ReadingGoal(_goalDate, goalNumber, items, timespan);
                        var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                        goalsList.Add(readingPerTimespanGoal);
                        bool successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                        if (successfulSave)
                        {
                            Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                        }
                    }
                    catch (Exception e)
                    {
                        Toast.MakeText(this, "Error: " + e.Message, ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Error: Date in past", ToastLength.Long).Show();
                }
            };
            #endregion

            #endregion

            #region Date Management

            predefinedGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = predefinedGoalTypeSpinner.GetItemAtPosition(predefinedGoalTypeSpinner.SelectedItemPosition);

                    readingGoalLayout.Visibility = currentItem.ToString() == "Reading" ? ViewStates.Visible : ViewStates.Gone;
                    fitnessGoalLayout.Visibility = currentItem.ToString() == "Fitness" ? ViewStates.Visible : ViewStates.Gone;
                };

            fitnessGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = fitnessGoalTypeSpinner.GetItemAtPosition(fitnessGoalTypeSpinner.SelectedItemPosition);

                    fitnessByDateLayout.Visibility = currentItem.ToString() == "By Date" ? ViewStates.Visible : ViewStates.Gone;
                    fitnessPerTimespanLayout.Visibility = currentItem.ToString() == "Per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };

            readingGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = readingGoalTypeSpinner.GetItemAtPosition(readingGoalTypeSpinner.SelectedItemPosition);

                    readingByDateLayout.Visibility = currentItem.ToString() == "By Date" ? ViewStates.Visible : ViewStates.Gone;
                    readingPerTimespanLayout.Visibility = currentItem.ToString() == "Per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };
        }

        private void UpdateReadingByDateDate()
        {
            _readingByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFitnessByDateDate()
        {
            _fitnessByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFitnessPerTimespanDate()
        {
            _fitnessPerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _goalDate = e.Date;
            UpdateReadingByDateDate();
            UpdateFitnessByDateDate();
            UpdateFitnessPerTimespanDate();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, _goalDate.Year, _goalDate.Month - 1, _goalDate.Day);
            }
            return null;
        }

        #endregion
    }
}