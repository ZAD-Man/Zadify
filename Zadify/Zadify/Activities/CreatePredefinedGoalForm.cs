using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Zadify.Enums;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class CreatePredefinedGoalForm : Activity
    {
        private const int DATE_DIALOG_ID = 0;

        private DateTime _goalDate = DateTime.Today;
        private Button _readingByDateSelectDate;
        private Button _readingPerTimespanSelectDate;
        private Button _writingByDateSelectDate;
        private Button _writingPerTimespanSelectDate;
        private Button _fitnessByDateSelectDate;
        private Button _fitnessPerTimespanSelectDate;
        private Button _financeSaveByDateSelectDate;
        private Button _financeSavePerTimespanSelectDate;
        private Button _financePayByDateSelectDate;
        private Button _financePayPerTimespanSelectDate;
        private Button _dietGainWeightSelectDate;
        private Button _dietLoseWeightSelectDate;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreatePredefinedGoalForm", "Create Predefined Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreatePredefinedGoalForm);

            var preferences = GetPreferences(FileCreationMode.Private);

            if (!preferences.Contains("Rank"))
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutInt("Rank", 0).Commit();
            }

            if (!preferences.Contains("MonsterMode"))
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutBoolean("MonsterMode", true).Commit();
            }

            var rank = preferences.GetInt("Rank", -1);
            var monsterMode = preferences.GetBoolean("MonsterMode", false);

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
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
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
                            case "Mile(s) Run":
                                items = FitnessItems.MilesRun;
                                break;
                            case "Kilometer(s) Run":
                                items = FitnessItems.KilometersRun;
                                break;
                        }

                        var fitnessByDateGoal = new FitnessGoal(_goalDate, goalNumber, items);
                        fitnessByDateGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(fitnessByDateGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(fitnessByDateGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
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
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(fitnessPerTimespanNumber.Text);
                        var items = FitnessItems.Pushups;
                        var timespan = 0;
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
                            case "Mile(s) Run":
                                items = FitnessItems.MilesRun;
                                break;
                            case "Kilometer(s) Run":
                                items = FitnessItems.KilometersRun;
                                break;
                        }

                        switch (selectedTimespan.ToString())
                        {
                            case "Day":
                                timespan = 1;
                                break;
                            case "Week":
                                timespan = 7;
                                break;
                            case "Month":
                                timespan = 30;
                                break;
                        }

                        var fitnessPerTimespanGoal = new FitnessGoal(_goalDate, goalNumber, items, timespan);
                        fitnessPerTimespanGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(fitnessPerTimespanGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(fitnessPerTimespanGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #endregion

            #region Diet Goals

            var dietGoalLayout = FindViewById<RelativeLayout>(Resource.Id.DietGoalLayout);

            var dietGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.DietGoalTypeSpinner);
            var dietGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.dietGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            dietGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            dietGoalTypeSpinner.Adapter = dietGoalTypeAdapter;

            #region Diet Gain Weight

            var dietGainWeightLayout = FindViewById<RelativeLayout>(Resource.Id.DietGainWeightLayout);

            var dietGainWeightNumber = FindViewById<EditText>(Resource.Id.DietGainWeightNumber);

            var dietGainWeightWeightsSpinner = FindViewById<Spinner>(Resource.Id.DietGainWeightItemsSpinner);
            var dietGainWeightWeightsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.dietWeights, Android.Resource.Layout.SimpleSpinnerItem);
            dietGainWeightWeightsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            dietGainWeightWeightsSpinner.Adapter = dietGainWeightWeightsAdapter;

            _dietGainWeightSelectDate = FindViewById<Button>(Resource.Id.DietGainWeightSelectDate);
            _dietGainWeightSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitDietGainWeightGoalButton = FindViewById<Button>(Resource.Id.SubmitDietGainWeightGoalButton);
            submitDietGainWeightGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(dietGainWeightNumber.Text);
                        var items = DietItems.Pounds;
                        var selectedItems = dietGainWeightWeightsSpinner.GetItemAtPosition(dietGainWeightWeightsSpinner.SelectedItemPosition);
                        switch (selectedItems.ToString())
                        {
                            case "Pound(s)":
                                items = DietItems.Pounds;
                                break;
                            case "Kilogram(s)":
                                items = DietItems.Kilograms;
                                break;
                        }

                        var dietGainWeightGoal = new DietGoal(_goalDate, goalNumber, items);
                        dietGainWeightGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(dietGainWeightGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(dietGainWeightGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Diet Lose Weight

            var dietLoseWeightLayout = FindViewById<RelativeLayout>(Resource.Id.DietLoseWeightLayout);

            var dietLoseWeightNumber = FindViewById<EditText>(Resource.Id.DietLoseWeightNumber);

            var dietLoseWeightItemsSpinner = FindViewById<Spinner>(Resource.Id.DietLoseWeightItemsSpinner);
            var dietLoseWeightItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.dietWeights, Android.Resource.Layout.SimpleSpinnerItem);
            dietLoseWeightItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            dietLoseWeightItemsSpinner.Adapter = dietLoseWeightItemsAdapter;

            _dietLoseWeightSelectDate = FindViewById<Button>(Resource.Id.DietLoseWeightSelectDate);
            _dietLoseWeightSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitDietLoseWeightGoalButton = FindViewById<Button>(Resource.Id.SubmitDietLoseWeightGoalButton);
            submitDietLoseWeightGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(dietLoseWeightNumber.Text);
                        var items = DietItems.Pounds;
                        var timespan = 0;
                        var selectedItems = dietLoseWeightItemsSpinner.GetItemAtPosition(dietLoseWeightItemsSpinner.SelectedItemPosition);
                        switch (selectedItems.ToString())
                        {
                            case "Pound(s)":
                                items = DietItems.Pounds;
                                break;
                            case "Kilogram(s)":
                                items = DietItems.Kilograms;
                                break;
                        }

                        var dietLoseWeightGoal = new DietGoal(_goalDate, goalNumber, items, timespan);
                        dietLoseWeightGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(dietLoseWeightGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(dietLoseWeightGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #endregion

            #region Finance Goals

            var financeGoalLayout = FindViewById<RelativeLayout>(Resource.Id.FinanceGoalLayout);

            var financeGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.FinanceGoalTypeSpinner);
            var financeGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.financeGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            financeGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            financeGoalTypeSpinner.Adapter = financeGoalTypeAdapter;

            #region Finance Save By Date

            var financeSaveByDateLayout = FindViewById<RelativeLayout>(Resource.Id.FinanceSaveByDateLayout);

            var financeSaveByDateNumber = FindViewById<EditText>(Resource.Id.FinanceSaveByDateNumber);

            _financeSaveByDateSelectDate = FindViewById<Button>(Resource.Id.FinanceSaveByDateSelectDate);
            _financeSaveByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFinanceSaveByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitFinanceSaveByDateGoalButton);
            submitFinanceSaveByDateGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(financeSaveByDateNumber.Text);

                        var financeSaveByDateGoal = new FinanceGoal(_goalDate, goalNumber);
                        financeSaveByDateGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(financeSaveByDateGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(financeSaveByDateGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Finance Save Per Timespan

            var financeSavePerTimespanLayout = FindViewById<RelativeLayout>(Resource.Id.FinanceSavePerTimespanLayout);

            var financeSavePerTimespanNumber = FindViewById<EditText>(Resource.Id.FinanceSavePerTimespanNumber);

            var financeSavePerTimespanTimespanSpinner = FindViewById<Spinner>(Resource.Id.FinanceSavePerTimespanTimespanSpinner);
            var financeSavePerTimespanTimespanAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.repeatingTimespans, Android.Resource.Layout.SimpleSpinnerItem);
            financeSavePerTimespanTimespanAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            financeSavePerTimespanTimespanSpinner.Adapter = financeSavePerTimespanTimespanAdapter;

            _financeSavePerTimespanSelectDate = FindViewById<Button>(Resource.Id.FinanceSavePerTimespanSelectDate);
            _financeSavePerTimespanSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFinanceSavePerTimespanGoalButton = FindViewById<Button>(Resource.Id.SubmitFinanceSavePerTimespanGoalButton);
            submitFinanceSavePerTimespanGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(financeSavePerTimespanNumber.Text);
                        var timespan = 0;

                        var selectedTimespan = financeSavePerTimespanTimespanSpinner.GetItemAtPosition(financeSavePerTimespanTimespanSpinner.SelectedItemPosition);

                        switch (selectedTimespan.ToString())
                        {
                            case "Day":
                                timespan = 1;
                                break;
                            case "Week":
                                timespan = 7;
                                break;
                            case "Month":
                                timespan = 30;
                                break;
                        }

                        var financeSavePerTimespanGoal = new FinanceGoal(_goalDate, goalNumber, timespan);
                        financeSavePerTimespanGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(financeSavePerTimespanGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(financeSavePerTimespanGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Finance Pay By Date

            var financePayByDateLayout = FindViewById<RelativeLayout>(Resource.Id.FinancePayByDateLayout);

            var financePayByDateNumber = FindViewById<EditText>(Resource.Id.FinancePayByDateNumber);

            _financePayByDateSelectDate = FindViewById<Button>(Resource.Id.FinancePayByDateSelectDate);
            _financePayByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFinancePayByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitFinancePayByDateGoalButton);
            submitFinancePayByDateGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(financePayByDateNumber.Text);

                        var financePayByDateGoal = new FinanceGoal(_goalDate, 0 - goalNumber);
                        financePayByDateGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(financePayByDateGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(financePayByDateGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Finance Pay Per Timespan

            var financePayPerTimespanLayout = FindViewById<RelativeLayout>(Resource.Id.FinancePayPerTimespanLayout);

            var financePayPerTimespanNumber = FindViewById<EditText>(Resource.Id.FinancePayPerTimespanNumber);

            var financePayPerTimespanTimespanSpinner = FindViewById<Spinner>(Resource.Id.FinancePayPerTimespanTimespanSpinner);
            var financePayPerTimespanTimespanAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.repeatingTimespans, Android.Resource.Layout.SimpleSpinnerItem);
            financePayPerTimespanTimespanAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            financePayPerTimespanTimespanSpinner.Adapter = financePayPerTimespanTimespanAdapter;

            _financePayPerTimespanSelectDate = FindViewById<Button>(Resource.Id.FinancePayPerTimespanSelectDate);
            _financePayPerTimespanSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitFinancePayPerTimespanGoalButton = FindViewById<Button>(Resource.Id.SubmitFinancePayPerTimespanGoalButton);
            submitFinancePayPerTimespanGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(financePayPerTimespanNumber.Text);
                        var timespan = 0;
                        var selectedTimespan = financePayPerTimespanTimespanSpinner.GetItemAtPosition(financePayPerTimespanTimespanSpinner.SelectedItemPosition);

                        switch (selectedTimespan.ToString())
                        {
                            case "Day":
                                timespan = 1;
                                break;
                            case "Week":
                                timespan = 7;
                                break;
                            case "Month":
                                timespan = 30;
                                break;
                        }

                        var financePayPerTimespanGoal = new FinanceGoal(_goalDate, 0 - goalNumber, timespan);
                        financePayPerTimespanGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(financePayPerTimespanGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(financePayPerTimespanGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
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
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var readingByDateGoal = new ReadingGoal(_goalDate, goalNumber, items);
                        readingByDateGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(readingByDateGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(readingByDateGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
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
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(readingPerTimespanNumber.Text);
                        var items = ReadingItems.Books;
                        var timespan = 0;
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
                                timespan = 1;
                                break;
                            case "Week":
                                timespan = 7;
                                break;
                            case "Month":
                                timespan = 30;
                                break;
                        }

                        var readingPerTimespanGoal = new ReadingGoal(_goalDate, goalNumber, items, timespan);
                        readingPerTimespanGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(readingPerTimespanGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(readingPerTimespanGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #endregion

            #region Writing Goals

            var writingGoalLayout = FindViewById<RelativeLayout>(Resource.Id.WritingGoalLayout);

            var writingGoalTypeSpinner = FindViewById<Spinner>(Resource.Id.WritingGoalTypeSpinner);
            var writingGoalTypeAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.writingGoalTypes, Android.Resource.Layout.SimpleSpinnerItem);
            writingGoalTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            writingGoalTypeSpinner.Adapter = writingGoalTypeAdapter;

            #region Writing By Date

            var writingByDateLayout = FindViewById<RelativeLayout>(Resource.Id.WritingByDateLayout);

            var writingByDateNumber = FindViewById<EditText>(Resource.Id.WritingByDateNumber);

            var writingByDateItemsSpinner = FindViewById<Spinner>(Resource.Id.WritingByDateItemsSpinner);
            var writingByDateItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.writingItems, Android.Resource.Layout.SimpleSpinnerItem);
            writingByDateItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            writingByDateItemsSpinner.Adapter = writingByDateItemsAdapter;

            _writingByDateSelectDate = FindViewById<Button>(Resource.Id.WritingByDateSelectDate);
            _writingByDateSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitWritingByDateGoalButton = FindViewById<Button>(Resource.Id.SubmitWritingByDateGoalButton);
            submitWritingByDateGoalButton.Click += delegate
                {
                    var goalNumber = int.Parse(writingByDateNumber.Text);
                    var items = WritingItems.Hours;
                    var selectedItems = writingByDateItemsSpinner.GetItemAtPosition(writingByDateItemsSpinner.SelectedItemPosition);
                    switch (selectedItems.ToString())
                    {
                        case "Hour(s)":
                            items = WritingItems.Hours;
                            break;
                        case "Minute(s)":
                            items = WritingItems.Minutes;
                            break;
                        case "Word(s)":
                            items = WritingItems.Words;
                            break;
                        case "Page(s)":
                            items = WritingItems.Pages;
                            break;
                    }
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var writingByDateGoal = new WritingGoal(_goalDate, goalNumber, items);
                        writingByDateGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(writingByDateGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(writingByDateGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #region Writing Per Timespan

            var writingPerTimespanLayout = FindViewById<RelativeLayout>(Resource.Id.WritingPerTimespanLayout);

            var writingPerTimespanNumber = FindViewById<EditText>(Resource.Id.WritingPerTimespanNumber);

            var writingPerTimespanItemsSpinner = FindViewById<Spinner>(Resource.Id.WritingPerTimespanItemsSpinner);
            var writingPerTimespanItemsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.writingItems, Android.Resource.Layout.SimpleSpinnerItem);
            writingPerTimespanItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            writingPerTimespanItemsSpinner.Adapter = writingPerTimespanItemsAdapter;

            var writingPerTimespanTimespanSpinner = FindViewById<Spinner>(Resource.Id.WritingPerTimespanTimespanSpinner);
            var writingPerTimespanTimespanAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.repeatingTimespans, Android.Resource.Layout.SimpleSpinnerItem);
            writingPerTimespanTimespanAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            writingPerTimespanTimespanSpinner.Adapter = writingPerTimespanTimespanAdapter;

            _writingPerTimespanSelectDate = FindViewById<Button>(Resource.Id.WritingPerTimespanSelectDate);
            _writingPerTimespanSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            var submitWritingPerTimespanGoalButton = FindViewById<Button>(Resource.Id.SubmitWritingPerTimespanGoalButton);
            submitWritingPerTimespanGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(writingPerTimespanNumber.Text);
                        var items = WritingItems.Hours;
                        var timespan = 0;
                        var selectedItems = writingPerTimespanItemsSpinner.GetItemAtPosition(writingPerTimespanItemsSpinner.SelectedItemPosition);
                        var selectedTimespan = writingPerTimespanTimespanSpinner.GetItemAtPosition(writingPerTimespanTimespanSpinner.SelectedItemPosition);
                        switch (selectedItems.ToString())
                        {
                            case "Hour(s)":
                                items = WritingItems.Hours;
                                break;
                            case "Minute(s)":
                                items = WritingItems.Minutes;
                                break;
                            case "Page(s)":
                                items = WritingItems.Pages;
                                break;
                            case "Word(s)":
                                items = WritingItems.Words;
                                break;
                        }

                        switch (selectedTimespan.ToString())
                        {
                            case "Day":
                                timespan = 1;
                                break;
                            case "Week":
                                timespan = 7;
                                break;
                            case "Month":
                                timespan = 30;
                                break;
                        }

                        var writingPerTimespanGoal = new WritingGoal(_goalDate, goalNumber, items, timespan);
                        writingPerTimespanGoal.AssignMonsterData(rank);
                        var successfulSave = SaveGoalToList(writingPerTimespanGoal);
                        if (successfulSave)
                        {
                            if (monsterMode)
                                MakeMonsterDialog(writingPerTimespanGoal);
                            Finish();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };

            #endregion

            #endregion

            #region Menu Management

            predefinedGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = predefinedGoalTypeSpinner.GetItemAtPosition(predefinedGoalTypeSpinner.SelectedItemPosition);

                    readingGoalLayout.Visibility = currentItem.ToString() == "Reading" ? ViewStates.Visible : ViewStates.Gone;
                    writingGoalLayout.Visibility = currentItem.ToString() == "Writing" ? ViewStates.Visible : ViewStates.Gone;
                    fitnessGoalLayout.Visibility = currentItem.ToString() == "Fitness" ? ViewStates.Visible : ViewStates.Gone;
                    financeGoalLayout.Visibility = currentItem.ToString() == "Finance" ? ViewStates.Visible : ViewStates.Gone;
                    dietGoalLayout.Visibility = currentItem.ToString() == "Diet" ? ViewStates.Visible : ViewStates.Gone;
                };

            fitnessGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = fitnessGoalTypeSpinner.GetItemAtPosition(fitnessGoalTypeSpinner.SelectedItemPosition);

                    fitnessByDateLayout.Visibility = currentItem.ToString() == "By Date" ? ViewStates.Visible : ViewStates.Gone;
                    fitnessPerTimespanLayout.Visibility = currentItem.ToString() == "Per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };

            financeGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = financeGoalTypeSpinner.GetItemAtPosition(financeGoalTypeSpinner.SelectedItemPosition);

                    financeSaveByDateLayout.Visibility = currentItem.ToString() == "Save Money by Date" ? ViewStates.Visible : ViewStates.Gone;
                    financeSavePerTimespanLayout.Visibility = currentItem.ToString() == "Save Money per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                    financePayByDateLayout.Visibility = currentItem.ToString() == "Pay Loan by Date" ? ViewStates.Visible : ViewStates.Gone;
                    financePayPerTimespanLayout.Visibility = currentItem.ToString() == "Pay Part of Loan per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };

            readingGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = readingGoalTypeSpinner.GetItemAtPosition(readingGoalTypeSpinner.SelectedItemPosition);

                    readingByDateLayout.Visibility = currentItem.ToString() == "By Date" ? ViewStates.Visible : ViewStates.Gone;
                    readingPerTimespanLayout.Visibility = currentItem.ToString() == "Per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };

            writingGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = writingGoalTypeSpinner.GetItemAtPosition(writingGoalTypeSpinner.SelectedItemPosition);

                    writingByDateLayout.Visibility = currentItem.ToString() == "By Date" ? ViewStates.Visible : ViewStates.Gone;
                    writingPerTimespanLayout.Visibility = currentItem.ToString() == "Per Timespan" ? ViewStates.Visible : ViewStates.Gone;
                };

            dietGoalTypeSpinner.ItemSelected += delegate
                {
                    var currentItem = dietGoalTypeSpinner.GetItemAtPosition(dietGoalTypeSpinner.SelectedItemPosition);

                    dietLoseWeightLayout.Visibility = currentItem.ToString() == "Lose Weight" ? ViewStates.Visible : ViewStates.Gone;
                    dietGainWeightLayout.Visibility = currentItem.ToString() == "Gain Weight" ? ViewStates.Visible : ViewStates.Gone;
                };

            #endregion
        }

        private bool SaveGoalToList(Goal goal)
        {
            try
            {
                var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                goalsList.Add(goal);
                var successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                if (successfulSave)
                {
                    Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Error saving goal", ToastLength.Long).Show();
                }

                return successfulSave;
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Error: " + e.Message, ToastLength.Long).Show();
                return false;
            }
        }

        private void MakeMonsterDialog(Goal goal)
        {
            var monsterDisplay = new Intent(this, typeof (MonsterDisplay));
            monsterDisplay.PutExtra("DisplayType", "Create");
            monsterDisplay.PutExtra("PercentDone", (int) (goal.Progress*100));
            monsterDisplay.PutExtra("Monster", goal.Monster);
            monsterDisplay.PutExtra("Food", goal.Food);
            monsterDisplay.PutExtra("Defense", goal.Defense);
            monsterDisplay.PutExtra("Weapon", goal.Weapon);
            StartActivity(monsterDisplay);
        }

        #region Date Management

        private void UpdateReadingByDateDate()
        {
            _readingByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateReadingPerTimespanDate()
        {
            _readingPerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateWritingByDateDate()
        {
            _writingByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateWritingPerTimespanDate()
        {
            _writingPerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFitnessByDateDate()
        {
            _fitnessByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFitnessPerTimespanDate()
        {
            _fitnessPerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFinanceSaveByDateDate()
        {
            _financeSaveByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFinanceSavePerTimespanDate()
        {
            _financeSavePerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFinancePayByDateDate()
        {
            _financePayByDateSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateFinancePayPerTimespanDate()
        {
            _financePayPerTimespanSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateDietGainWeightDate()
        {
            _dietGainWeightSelectDate.Text = _goalDate.ToString("d");
        }

        private void UpdateDietLoseWeightDate()
        {
            _dietLoseWeightSelectDate.Text = _goalDate.ToString("d");
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _goalDate = e.Date;
            UpdateReadingByDateDate();
            UpdateReadingPerTimespanDate();
            UpdateWritingByDateDate();
            UpdateWritingPerTimespanDate();
            UpdateFitnessByDateDate();
            UpdateFitnessPerTimespanDate();
            UpdateFinanceSaveByDateDate();
            UpdateFinanceSavePerTimespanDate();
            UpdateFinancePayByDateDate();
            UpdateFinancePayPerTimespanDate();
            UpdateDietLoseWeightDate();
            UpdateDietGainWeightDate();
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