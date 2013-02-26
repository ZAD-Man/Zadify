using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Ranks")]
    public class RanksMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("RanksMenu", "Ranks Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RanksMenu);

            var preferences = GetPreferences(FileCreationMode.Private);
            var storedRank = preferences.GetInt("Rank", -1);

            if (storedRank == -1)
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutInt("Rank", 1).Commit();
            }

            var CurrentRank = FindViewById<TextView>(Resource.Id.CurrentRank);
            int finishedGoalCount = 0;
            int amountToNextRank = 1;
            var rank = "Noob";
            try
            {
                var goalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (goalList != null)
                {
                    foreach (var goal in goalList)
                    {
                        if (goal.GoalCompletedAmount == goal.GoalAmount)
                        {
                            finishedGoalCount++;
                        }
                    }
                }
            }


            catch (Exception e)
            {
                Log.Error("RanksMenu", e.Message + e.StackTrace);
            }

            if (finishedGoalCount > 100)
            {
                rank = "General";
            }
            else if (finishedGoalCount > 50)
            {
                rank = "Captain";
                amountToNextRank = 100 - finishedGoalCount;
            }
            else if (finishedGoalCount > 15)
            {
                rank = "Manager";
                amountToNextRank = 50 - finishedGoalCount;
            }
            else if (finishedGoalCount > 5)
            {
                rank = "Swabbie";
                amountToNextRank = 15 - finishedGoalCount;
            }
            else if (finishedGoalCount > 1)
            {
                rank = "Starter";
                amountToNextRank = 5 - finishedGoalCount;
            }

            CurrentRank.Text = "You are currently ranked " + rank + ".\nKeep completing goals!\n(" + amountToNextRank + " goals to the next rank!)";
        }
    }
}