using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Goal Details")] //TODO: Replace with name of selected goal
    public class GoalDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("GoalDetailsScreen", "Goal Details Screen Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.GoalDetailsScreen);

            var position = Intent.GetIntExtra("Position", -1);

            if (position != -1)
            {
                var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoals != null)
                {
                    var displayGoal = storedGoals[position];
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
                                    goalDetailsActionText.Text = "Gain " + dietGoal.GoalAmount + " " + dietGoal.MeasuredItems;
                                }
                                else
                                {
                                    goalDetailsActionText.Text = "Lose " + Math.Abs(dietGoal.GoalAmount) + " " + dietGoal.MeasuredItems;
                                }
                            }
                            break;
                        case "FinanceGoal":
                            {
                                goalDetailsTitle.Text = "Finance Goal";
                                var financeGoal = (FinanceGoal) displayGoal;
                                if (financeGoal.GoalAmount > 0)
                                {
                                    //TODO: Save
                                    goalDetailsActionText.Text = "Save $" + financeGoal.GoalAmount;
                                }
                                else if (financeGoal.GoalAmount < 0)
                                {
                                    //TODO: Pay
                                    goalDetailsActionText.Text = "Pay $" + Math.Abs(financeGoal.GoalAmount);
                                }
                            }
                            break;
                        case "FitnessGoal":
                            {
                                goalDetailsTitle.Text = "Fitness Goal";
                                var fitnessGoal = (FitnessGoal) displayGoal;
                                goalDetailsActionText.Text = "Do " + fitnessGoal.GoalAmount + " " + fitnessGoal.MeasuredItems;
                            }
                            break;
                        case "ReadingGoal":
                            {
                                goalDetailsTitle.Text = "Reading Goal";
                                var readingGoal = (ReadingGoal) displayGoal;
                                goalDetailsActionText.Text = "Read " + readingGoal.GoalAmount + " " + readingGoal.MeasuredItems;
                            }
                            break;
                        case "WritingGoal":
                            {
                                goalDetailsTitle.Text = "Writing Goal";
                                var writingGoal = (WritingGoal) displayGoal;
                                goalDetailsActionText.Text = "Write " + writingGoal.GoalAmount + " " + writingGoal.MeasuredItems;
                            }
                            break;
                        default:
                            Log.Error("GoalsDetailsScreen", "Error with goal " + displayGoal);
                            Log.Error("GoalsDetailsScreen", "Invalid goal type: " + goalType);
                            break;
                    }

                    var repeatingDaysDays = displayGoal.RepeatingDays;
                    Log.Debug("GoalDetailsrepeatingDays", repeatingDaysDays.ToString());
                    switch (repeatingDaysDays)
                    {
                        case 0:
                            goalDetailsTimespanText.Visibility = ViewStates.Gone;
                            break;
                        case 1:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "Every day";
                            break;
                        case 7:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "Every week";
                            break;
                        case 30:
                            goalDetailsTimespanText.Visibility = ViewStates.Visible;
                            goalDetailsTimespanText.Text = "Every month";
                            break;
                    }

                    if (repeatingDaysDays > 0)
                    {
                        goalDetailsDueDateText.Text = "Until " + displayGoal.DueDate.ToString("d");
                    }
                    else
                    {
                        goalDetailsDueDateText.Text = "By " + displayGoal.DueDate.ToString("d");
                    }

                    goalDetailsAmountCompletedText.Text = displayGoal.GoalCompletedAmount + "/" + displayGoal.GoalAmount + " done (" + (int)displayGoal.Progress * 100 + "%)";
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

            var updateGoalButton = FindViewById<Button>(Resource.Id.UpdateGoalButton);
            updateGoalButton.Click += delegate { StartActivity(typeof (UpdateGoalForm)); };

            var deleteGoalButton = FindViewById<Button>(Resource.Id.DeleteGoalButton);
            deleteGoalButton.Click += delegate { StartActivity(typeof (DeleteGoalForm)); };
        }
    }
}