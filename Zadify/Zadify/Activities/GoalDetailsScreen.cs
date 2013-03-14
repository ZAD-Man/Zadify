using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Goal Details")]
    public class GoalDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("GoalDetailsScreen", "Goal Details Screen Created");

            base.OnCreate(bundle);
        }

        protected override void OnStart()
        {
            Log.Info("GoalDetailsScreen", "Goal Details Screen Started");

            base.OnStart();

            SetContentView(Resource.Layout.GoalDetailsScreen);

            var layout = FindViewById<LinearLayout>(Resource.Id.GoalDetailsScreenLayout);
            layout.SetBackgroundResource(Resource.Color.darkred);

            var position = Intent.GetIntExtra("Position", -1);
            var isCompleted = Intent.GetBooleanExtra("IsCompleted", false);

            if (position != -1)
            {
                var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoals != null)
                {
                    var sortedGoals = new List<Goal>();
                    if (isCompleted)
                    {
                        sortedGoals.AddRange(storedGoals.Where(goal => goal.ViewedPostDueDate));
                    }
                    else
                    {
                        sortedGoals.AddRange(storedGoals.Where(goal => !goal.ViewedPostDueDate));
                    }

                    var displayGoal = sortedGoals[position];
                    var goalType = displayGoal.GetType().Name;
                    var goalDetailsTitle = FindViewById<TextView>(Resource.Id.GoalDetailsTitle);
                    var goalDetailsActionText = FindViewById<TextView>(Resource.Id.GoalDetailsActionText);
                    var goalDetailsTimespanText = FindViewById<TextView>(Resource.Id.GoalDetailsTimespanText);
                    var goalDetailsDueDateText = FindViewById<TextView>(Resource.Id.GoalDetailsDueDateText);
                    var goalDetailsAmountCompletedText = FindViewById<TextView>(Resource.Id.GoalDetailsAmountCompletedText);

                    switch (goalType)
                    {
                        case "DietGoal":
                            {
                                goalDetailsTitle.Text = "Diet Goal";
                                var dietGoal = (DietGoal) displayGoal;
                                if (dietGoal.GoalAmount > 0)
                                {
                                    goalDetailsActionText.Text = "Gain " + dietGoal.GoalAmount + " " + dietGoal.MeasuredItems.ToString().ToLower();
                                }
                                else
                                {
                                    goalDetailsActionText.Text = "Lose " + Math.Abs(dietGoal.GoalAmount) + " " + dietGoal.MeasuredItems.ToString().ToLower();
                                }
                            }
                            break;
                        case "FinanceGoal":
                            {
                                goalDetailsTitle.Text = "Finance Goal";
                                var financeGoal = (FinanceGoal) displayGoal;
                                if (financeGoal.GoalAmount > 0)
                                {
                                    goalDetailsActionText.Text = "Save $" + financeGoal.GoalAmount;
                                }
                                else if (financeGoal.GoalAmount < 0)
                                {
                                    goalDetailsActionText.Text = "Pay $" + Math.Abs(financeGoal.GoalAmount);
                                }
                            }
                            break;
                        case "FitnessGoal":
                            {
                                goalDetailsTitle.Text = "Fitness Goal";
                                var fitnessGoal = (FitnessGoal) displayGoal;
                                goalDetailsActionText.Text = "Do " + fitnessGoal.GoalAmount + " " + fitnessGoal.MeasuredItems.ToString().ToLower();
                            }
                            break;
                        case "ReadingGoal":
                            {
                                goalDetailsTitle.Text = "Reading Goal";
                                var readingGoal = (ReadingGoal) displayGoal;
                                goalDetailsActionText.Text = "Read " + readingGoal.GoalAmount + " " + readingGoal.MeasuredItems.ToString().ToLower();
                            }
                            break;
                        case "WritingGoal":
                            {
                                goalDetailsTitle.Text = "Writing Goal";
                                var writingGoal = (WritingGoal) displayGoal;
                                goalDetailsActionText.Text = "Write " + writingGoal.GoalAmount + " " + writingGoal.MeasuredItems.ToString().ToLower();
                            }
                            break;
                        case "CustomGoal":
                            {
                                goalDetailsTitle.Text = "Custom Goal";
                                var customGoal = (CustomGoal) displayGoal;
                                goalDetailsActionText.Text = "Do " + customGoal.GoalAmount + " " + customGoal.MeasuredItems.ToLower();
                                break;
                            }
                        default:
                            Log.Error("GoalsDetailsScreen", "Error with goal " + displayGoal);
                            Log.Error("GoalsDetailsScreen", "Invalid goal type: " + goalType);
                            break;
                    }

                    var repeatingDays = displayGoal.RepeatingDays;
                    Log.Debug("GoalDetailsRepeatingDays", repeatingDays.ToString());
                    switch (repeatingDays)
                    {
                        case 0:
                            goalDetailsTimespanText.Visibility = ViewStates.Gone;
                            break;
                        case 1:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "every day";
                            break;
                        case 7:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "every week";
                            break;
                        case 30:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "every month";
                            break;
                        default:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "every " + repeatingDays + " days";
                            break;
                    }

                    if (repeatingDays > 0)
                    {
                        goalDetailsDueDateText.Text = "until " + displayGoal.DueDate.ToString("d");
                    }
                    else
                    {
                        goalDetailsDueDateText.Text = "by " + displayGoal.DueDate.ToString("d");
                    }

                    goalDetailsAmountCompletedText.Text = Math.Abs(displayGoal.GoalCompletedAmount) + "/" + Math.Abs(displayGoal.GoalAmount) + " done (" + (int) (displayGoal.Progress*100) + "%)";

                    var updateGoalButton = FindViewById<Button>(Resource.Id.UpdateGoalButton);

                    if (!displayGoal.IsPastDue())
                    {
                        updateGoalButton.Click += delegate
                            {
                                var updateGoalForm = new Intent(this, typeof (UpdateGoalForm));
                                updateGoalForm.PutExtra("Position", position);
                                StartActivity(updateGoalForm);
                            };
                    }
                    else
                    {
                        updateGoalButton.Visibility = ViewStates.Gone;
                    }

                    var deleteGoalButton = FindViewById<Button>(Resource.Id.DeleteGoalButton);
                    deleteGoalButton.Click += delegate
                        {
                            var deleteGoalForm = new Intent(this, typeof (DeleteGoalForm));
                            deleteGoalForm.PutExtra("Position", position);
                            StartActivity(deleteGoalForm);
                            Finish();
                        };
                }
                else
                {
                    Log.Error("GoalDetailsScreen:loadError", "Goals not loaded");
                }
            }
            else
            {
                Log.Error("GoalDetailsScreen:IntentError", "Position is -1, intent not found");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            var position = Intent.GetIntExtra("Position", -1);
            var isCompleted = Intent.GetBooleanExtra("IsCompleted", false);

            if (position != -1)
            {
                var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoals != null)
                {
                    var sortedGoals = new List<Goal>();
                    if (isCompleted)
                    {
                        sortedGoals.AddRange(storedGoals.Where(goal => goal.ViewedPostDueDate));
                    }
                    else
                    {
                        sortedGoals.AddRange(storedGoals.Where(goal => !goal.ViewedPostDueDate));
                    }

                    var displayGoal = sortedGoals[position];

                    if (displayGoal.IsPastDue())
                    {
                        displayGoal.Viewed();
                        storedGoals[storedGoals.IndexOf(displayGoal)] = displayGoal;
                        JavaIO.SaveData(this, "Goals.zad", storedGoals);
                    }
                }
            }
        }
    }
}