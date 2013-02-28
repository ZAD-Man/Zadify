using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Goals")]
    public class GoalsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("GoalsMenu", "Goals Menu Created");

            base.OnCreate(bundle);
        }

        protected override void OnStart()
        {
            Log.Info("GoalsMenu", "Goals Menu Started");

            base.OnStart();

            SetContentView(Resource.Layout.GoalsMenu);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);

            var monsterMode = preferences.GetBoolean("MonsterMode", false);

            var createGoalButton = FindViewById<Button>(Resource.Id.CreateGoalButton);
            createGoalButton.Click += delegate { StartActivity(typeof (CreateGoalMenu)); };

            var completedGoalsButton = FindViewById<Button>(Resource.Id.CompletedGoalsButton);
            completedGoalsButton.Click += delegate { StartActivity(typeof (CompletedGoalsMenu)); };

            try
            {
                var goalsList = FindViewById<ListView>(Resource.Id.GoalsList);
                var storedGoalStrings = new List<string>();
                var storedGoalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoalList != null)
                {
                    foreach (var goal in storedGoalList.Where(goal => !goal.ViewedPostDueDate))
                    {
                        if (goal is DietGoal)
                        {
                            var dietGoal = (DietGoal) goal;

                            var displayString = dietGoal.Summary();
                            displayString = AddDoneIfDone(dietGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else if (goal is FinanceGoal)
                        {
                            var financeGoal = (FinanceGoal) goal;

                            var displayString = financeGoal.Summary();
                            displayString = AddDoneIfDone(financeGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else if (goal is FitnessGoal)
                        {
                            var fitnessGoal = (FitnessGoal) goal;

                            var displayString = fitnessGoal.Summary();
                            displayString = AddDoneIfDone(fitnessGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else if (goal is ReadingGoal)
                        {
                            var readingGoal = (ReadingGoal) goal;

                            var displayString = readingGoal.Summary();
                            displayString = AddDoneIfDone(readingGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else if (goal is WritingGoal)
                        {
                            var writingGoal = (WritingGoal) goal;

                            var displayString = writingGoal.Summary();
                            displayString = AddDoneIfDone(writingGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else if (goal is CustomGoal)
                        {
                            var customGoal = (CustomGoal) goal;

                            var displayString = customGoal.Summary();
                            displayString = AddDoneIfDone(customGoal, displayString);
                            storedGoalStrings.Add(displayString);
                        }
                        else
                        {
                            Log.Error("GoalsMenu", "Can't cast goal " + goal);
                        }
                    }
                    var goalsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, storedGoalStrings);
                    goalsList.Adapter = goalsAdapter;

                    goalsList.ItemClick += (sender, args) =>
                        {
                            var position = args.Position;
                            var goalDetailsScreen = new Intent(this, typeof (GoalDetailsScreen));
                            goalDetailsScreen.PutExtra("Position", position);
                            goalDetailsScreen.PutExtra("IsCompleted", false);
                            StartActivity(goalDetailsScreen);

                            var sortedGoals = new List<Goal>();
                            sortedGoals.AddRange(storedGoalList.Where(goal => !goal.ViewedPostDueDate));

                            var displayGoal = sortedGoals[position];

                            if (displayGoal.DueDate.CompareTo(DateTime.Today) <= 0 && monsterMode)
                            {
                                MakeMonsterDialog(displayGoal);
                            }
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

        private void MakeMonsterDialog(Goal goal)
        {
            var monsterDisplay = new Intent(this, typeof (MonsterDisplay));
            monsterDisplay.PutExtra("DisplayType", "Complete");
            monsterDisplay.PutExtra("PercentDone", (int) (goal.Progress*100));
            monsterDisplay.PutExtra("Monster", goal.Monster);
            monsterDisplay.PutExtra("Food", goal.Food);
            monsterDisplay.PutExtra("Defense", goal.Defense);
            monsterDisplay.PutExtra("Weapon", goal.Weapon);
            StartActivity(monsterDisplay);
        }

        private string AddDoneIfDone(Goal goal, string displayString)
        {
            var doneString = displayString;
            if (goal.DueDate.CompareTo(DateTime.Today) <= 0)
            {
                doneString = "(Done)" + displayString;
            }
            return doneString;
        }
    }
}