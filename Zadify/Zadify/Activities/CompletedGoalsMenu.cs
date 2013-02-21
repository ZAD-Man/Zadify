using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Completed Goals")]
    public class CompletedGoalsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CompletedGoalsMenu", "Completed Goals Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CompletedGoalsMenu);

            try
            {
                var completedGoalsList = FindViewById<ListView>(Resource.Id.CompletedGoalsList);
                var storedGoalStrings = new List<string>();
                var storedGoalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoalList != null)
                {
                    foreach (var goal in storedGoalList.Where(goal => goal.ViewedPostDueDate))
                    {
                        if (goal is DietGoal)
                        {
                            var dietGoal = (DietGoal)goal;
                            if (dietGoal.GoalAmount > 0)
                            {
                                storedGoalStrings.Add("Gain " + dietGoal.GoalAmount + " " + dietGoal.MeasuredItems.ToString().ToLower() + " - " + (int)(dietGoal.Progress * 100) + "%");
                            }
                            else if (dietGoal.GoalAmount < 0)
                            {
                                storedGoalStrings.Add("Lose " + Math.Abs(dietGoal.GoalAmount) + " " + dietGoal.MeasuredItems.ToString().ToLower() + " - " + (int)(dietGoal.Progress * 100) + "%");
                            }
                        }
                        else if (goal is FinanceGoal)
                        {
                            var financeGoal = (FinanceGoal)goal;
                            if (financeGoal.GoalAmount > 0)
                            {
                                storedGoalStrings.Add("Save $" + financeGoal.GoalAmount + " - " + (int)(financeGoal.Progress * 100) + "%");
                            }
                            else if (financeGoal.GoalAmount < 0)
                            {
                                storedGoalStrings.Add("Pay $" + Math.Abs(financeGoal.GoalAmount) + " - " + (int)(financeGoal.Progress * 100) + "%");
                            }
                        }
                        else if (goal is FitnessGoal)
                        {
                            var fitnessGoal = (FitnessGoal)goal;

                            storedGoalStrings.Add("Do " + fitnessGoal.GoalAmount + " " + fitnessGoal.MeasuredItems.ToString().ToLower() + " - " + (int)(fitnessGoal.Progress * 100) + "%");
                        }
                        else if (goal is ReadingGoal)
                        {
                            var readingGoal = (ReadingGoal)goal;
                            storedGoalStrings.Add("Read " + readingGoal.GoalAmount + " " + readingGoal.MeasuredItems.ToString().ToLower() + " - " + (int)(readingGoal.Progress * 100) + "%");
                        }
                        else if (goal is WritingGoal)
                        {
                            var writingGoal = (WritingGoal)goal;
                            storedGoalStrings.Add("Write " + writingGoal.GoalAmount + " " + writingGoal.MeasuredItems.ToString().ToLower() + " - " + (int)(writingGoal.Progress * 100) + "%");
                        }
                        else if (goal is CustomGoal)
                        {
                            var customGoal = (CustomGoal)goal;
                            storedGoalStrings.Add("Do " + customGoal.GoalAmount + " " + customGoal.MeasuredItems.ToLower() + " - " + (int)(customGoal.Progress * 100) + "%");
                        }
                        else
                        {
                            Log.Error("GoalsMenu", "Can't cast goal " + goal);
                        }
                    }
                    var completedGoalsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, storedGoalStrings);
                    completedGoalsList.Adapter = completedGoalsAdapter;

                    completedGoalsList.ItemClick += (sender, args) =>
                    {
                        var goalDetailsScreen = new Intent(this, typeof(GoalDetailsScreen));
                        goalDetailsScreen.PutExtra("Position", args.Position);
                        goalDetailsScreen.PutExtra("IsCompleted", true);
                        StartActivity(goalDetailsScreen);
                    };
                }
            }
            catch (Java.IO.FileNotFoundException e)
            {
                Log.Error("GoalsMenu:FileNotFound", e.Message + e.StackTrace);
                Toast.MakeText(this, "No goals to display", ToastLength.Long).Show();
            }
            catch (Exception e)
            {
                Log.Error("GoalsMenu:GeneralException", e.Message + e.StackTrace);
                Toast.MakeText(this, "Goals could not be displayed", ToastLength.Long).Show();
            }
        }
    }
}