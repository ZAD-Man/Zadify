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
    [Activity(Label = "Ranks")]
    public class RanksMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("RanksMenu", "Ranks Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RanksMenu);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);

            if (!preferences.Contains("Rank"))
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            var storedRank = preferences.GetInt("Rank", -1);

            var currentRank = FindViewById<TextView>(Resource.Id.CurrentRank);
            int finishedGoalCount = 0;
            int amountToNextRank = 1;
            var rank = "Noob";
            try
            {
                var goalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (goalList != null)
                {
                    finishedGoalCount += goalList.Count(goal => goal.GoalCompletedAmount == goal.GoalAmount);
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

            currentRank.Text = "You are currently ranked " + rank + ".\nKeep completing goals!\n(" + amountToNextRank + " goals to the next rank!)";
        }
    }
}