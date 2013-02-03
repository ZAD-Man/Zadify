using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
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

            SetContentView(Resource.Layout.GoalsMenu);

            var createGoalButton = FindViewById<Button>(Resource.Id.CreateGoalButton);
            createGoalButton.Click += delegate { StartActivity(typeof (CreateGoalMenu)); };

            var completedGoalsButton = FindViewById<Button>(Resource.Id.CompletedGoalsButton);
            completedGoalsButton.Click += delegate { StartActivity(typeof (CompletedGoalsMenu)); };
        }

        protected override void OnStart()
        {
            Log.Info("GoalsMenu", "Goals Menu Started");

            base.OnStart();

            try
            {
                var goalsList = FindViewById<ListView>(Resource.Id.GoalsList);
                var storedGoalStrings = new List<string>();
                var storedGoalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (storedGoalList != null)
                {
                    foreach (var goal in storedGoalList)
                    {
                        if (goal is DietGoal)
                        {
                            var dietGoal = (DietGoal)goal;
                            storedGoalStrings.Add("Diet:" + dietGoal.MeasuredItems);
                        }
                        else if (goal is FinanceGoal)
                        {
                            var financeGoal = (FinanceGoal)goal;
                            storedGoalStrings.Add("Finance:" + financeGoal.MeasuredItems);
                        }
                        else if (goal is FitnessGoal)
                        {
                            var fitnessGoal = (FitnessGoal)goal;
                            storedGoalStrings.Add("Fitness:" + fitnessGoal.MeasuredItems);
                        }
                        else if (goal is ReadingGoal)
                        {
                            var readingGoal = (ReadingGoal)goal;
                            storedGoalStrings.Add("Reading:" + readingGoal.MeasuredItems);
                        }
                        else if (goal is WritingGoal)
                        {
                            var writingGoal = (WritingGoal)goal;
                            storedGoalStrings.Add("Writing:" + writingGoal.MeasuredItems);
                        }
                        else
                        {
                            Log.Error("GoalsMenu", "Can't cast goal " + goal.ToString());
                        }
                    }
                    var goalsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, storedGoalStrings);
                    goalsList.Adapter = goalsAdapter;
                }
            }
            catch (Java.IO.FileNotFoundException e)
            {
                Log.Error("GoalsMenu:FileNotFound", e.Message + e.StackTrace);
                Toast.MakeText(this, "No goals to display", ToastLength.Long);
            }
            catch (Exception e)
            {
                Log.Error("GoalsMenu:GeneralException", e.Message + e.StackTrace);
                Toast.MakeText(this, "Goals could not be displayed", ToastLength.Long);
            }

        }
    }
}