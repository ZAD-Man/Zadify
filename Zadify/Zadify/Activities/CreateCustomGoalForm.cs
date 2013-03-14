using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Create Goal")]
    public class CreateCustomGoalForm : Activity
    {
        private const int DATE_DIALOG_ID = 0;

        private DateTime _goalDate = DateTime.Today;
        private Button _customGoalSelectDate;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CreateCustomeGoalForm", "Create Custom Goal Form created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateCustomGoalForm);

            var layout = FindViewById<LinearLayout>(Resource.Id.CreateCustomGoalFormLayout);
            layout.SetBackgroundResource(Resource.Color.darkred);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);
            var monsterMode = preferences.GetBoolean("MonsterMode", false);

            if (!preferences.Contains("Rank"))
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            var rank = preferences.GetInt("Rank", -1);

            var customGoalNumber = FindViewById<EditText>(Resource.Id.CustomGoalNumber);

            var customGoalItems = FindViewById<EditText>(Resource.Id.CustomGoalItems);

            var customGoalRepeatCheckbox = FindViewById<CheckBox>(Resource.Id.CustomGoalRepeatCheckbox);

            var customGoalInnerLayout2 = FindViewById<LinearLayout>(Resource.Id.CustomGoalInnerLayout2);
            customGoalInnerLayout2.Visibility = ViewStates.Gone;

            var customGoalDaysNumber = FindViewById<EditText>(Resource.Id.CustomGoalDaysNumber);

            var customGoalText4 = FindViewById<TextView>(Resource.Id.CustomGoalText4);

            _customGoalSelectDate = FindViewById<Button>(Resource.Id.CustomGoalSelectDate);
            _customGoalSelectDate.Click += delegate { ShowDialog(DATE_DIALOG_ID); };

            customGoalRepeatCheckbox.Click += delegate
                {
                    if (customGoalRepeatCheckbox.Checked)
                    {
                        customGoalInnerLayout2.Visibility = ViewStates.Visible;
                        customGoalText4.Text = "until";
                    }
                    else
                    {
                        customGoalInnerLayout2.Visibility = ViewStates.Gone;
                        customGoalText4.Text = "by";
                    }
                };

            var submitCustomGoalButton = FindViewById<Button>(Resource.Id.SubmitCustomGoalButton);
            submitCustomGoalButton.Click += delegate
                {
                    if (_goalDate.CompareTo(DateTime.Today) > 0)
                    {
                        var goalNumber = int.Parse(customGoalNumber.Text);
                        var items = customGoalItems.Text;
                        var timespan = 0;
                        if (customGoalRepeatCheckbox.Checked && customGoalDaysNumber.Text != "")
                        {
                            timespan = int.Parse(customGoalDaysNumber.Text);
                        }

                        try
                        {
                            var customGoal = new CustomGoal(_goalDate, goalNumber, items, timespan);
                            customGoal.AssignMonsterData(rank);
                            var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                            goalsList.Add(customGoal);
                            var successfulSave = JavaIO.SaveData(this, "Goals.zad", goalsList);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Saved", ToastLength.Long).Show();
                                if (monsterMode)
                                    MakeMonsterDialog(customGoal);
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
                        Toast.MakeText(this, "Error: Date must be in future", ToastLength.Long).Show();
                    }
                };
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

        private void CustomGoalDate()
        {
            _customGoalSelectDate.Text = _goalDate.ToString("d");
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _goalDate = e.Date;
            CustomGoalDate();
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
    }
}