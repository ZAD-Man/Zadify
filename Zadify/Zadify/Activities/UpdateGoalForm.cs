using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Update Goal")]
    public class UpdateGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("UpdateGoalForm", "Update Goal Form Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UpdateGoalForm);

            var position = Intent.GetIntExtra("Position", -1);

            if (position != -1)
            {
                var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoals != null)
                {
                    var updateableGoals = new List<Goal>();
                    updateableGoals.AddRange(storedGoals.Where(goal => !goal.ViewedPostDueDate));

                    var updateGoal = updateableGoals[position];
                    var goalType = updateGoal.GetType().Name;
                    var updateGoalText = FindViewById<TextView>(Resource.Id.UpdateGoalText);

                    switch (goalType)
                    {
                        case "DietGoal":
                            {
                                var dietGoal = (DietGoal) updateGoal;
                                if (dietGoal.GoalAmount > 0)
                                {
                                    updateGoalText.Text = "How many " + dietGoal.MeasuredItems.ToString().ToLower() + " did you gain?";
                                }
                                else
                                {
                                    updateGoalText.Text = "How many " + dietGoal.MeasuredItems.ToString().ToLower() + " did you lose?";
                                }
                                break;
                            }
                        case "FinanceGoal":
                            {
                                var financeGoal = (FinanceGoal) updateGoal;
                                if (financeGoal.GoalAmount > 0)
                                {
                                    updateGoalText.Text = "How much did you save? $";
                                }
                                else if (financeGoal.GoalAmount < 0)
                                {
                                    updateGoalText.Text = "How much did you pay off? $";
                                }
                                break;
                            }
                        case "FitnessGoal":
                            {
                                var fitnessGoal = (FitnessGoal) updateGoal;
                                updateGoalText.Text = "How many " + fitnessGoal.MeasuredItems.ToString().ToLower() + " did you do?";
                                break;
                            }
                        case "ReadingGoal":
                            {
                                var readingGoal = (ReadingGoal) updateGoal;
                                updateGoalText.Text = "How many " + readingGoal.MeasuredItems.ToString().ToLower() + " did you read?";
                                break;
                            }
                        case "WritingGoal":
                            {
                                var writingGoal = (WritingGoal) updateGoal;
                                updateGoalText.Text = "How many " + writingGoal.MeasuredItems.ToString().ToLower() + " did you write?";
                                break;
                            }
                        case "CustomGoal":
                            {
                                var customGoal = (CustomGoal) updateGoal;
                                updateGoalText.Text = "How many " + customGoal.MeasuredItems.ToLower() + " did you do?";
                                break;
                            }
                        default:
                            Log.Error("GoalsDetailsScreen", "Error with goal " + updateGoal);
                            Log.Error("GoalsDetailsScreen", "Invalid goal type: " + goalType);
                            break;
                    }
                    var updateGoalSubmitButton = FindViewById<Button>(Resource.Id.UpdateGoalSubmitButton);
                    updateGoalSubmitButton.Click += delegate
                        {
                            var updateGoalNumber = FindViewById<EditText>(Resource.Id.UpdateGoalNumber);
                            var updateNumber = int.Parse(updateGoalNumber.Text);
                            var goalAmount = updateGoal.GoalAmount;
                            if (goalAmount > 0)
                            {
                                updateNumber += updateGoal.GoalCompletedAmount;
                                updateGoal.UpdateProgress(updateNumber);
                            }
                            else
                            {
                                updateNumber = 0 - updateNumber;
                                updateNumber += updateGoal.GoalCompletedAmount;
                                updateGoal.UpdateProgress(updateNumber);
                            }
                            storedGoals[position] = updateGoal;
                            bool successfulSave = JavaIO.SaveData(this, "Goals.zad", storedGoals);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Updated", ToastLength.Long).Show();
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Error updating goal", ToastLength.Long).Show();
                            }
                        };
                }
                else
                {
                    Log.Error("UpdateGoalForm:loadError", "Goals not loaded");
                }
            }
            else
            {
                Log.Error("UpdateGoalForm:IntentError", "Position is -1, intent not found");
            }
        }
    }
}