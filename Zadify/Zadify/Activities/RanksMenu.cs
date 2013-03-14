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
                var preferencesEditor = preferences.Edit();

            if (!preferences.Contains("Rank"))
            {
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            var storedRank = preferences.GetInt("Rank", -1);

            var rankName = FindViewById<TextView>(Resource.Id.RankName);
            var toNextRank = FindViewById<TextView>(Resource.Id.ToNextRank);
            int finishedGoalCount = 0;
            int amountToNextRank = -1;
            var rank = "Noob";
            try
            {
                var goalList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (goalList != null)
                {
                    finishedGoalCount += goalList.Count(goal => goal.IsCompleted());
                }
            }
            catch (Exception e)
            {
                Log.Error("RanksMenu", e.Message + e.StackTrace);
            }

            if (finishedGoalCount >= 100)
            {
                rank = "General";
                storedRank = 5;
            }
            else if (finishedGoalCount >= 50)
            {
                rank = "Captain";
                amountToNextRank = 100 - finishedGoalCount;
                storedRank = 4;
            }
            else if (finishedGoalCount >= 15)
            {
                rank = "Manager";
                amountToNextRank = 50 - finishedGoalCount;
                storedRank = 3;
            }
            else if (finishedGoalCount >= 5)
            {
                rank = "Swabbie";
                amountToNextRank = 15 - finishedGoalCount;
                storedRank = 2;
            }
            else if (finishedGoalCount >= 1)
            {
                rank = "Beginner";
                amountToNextRank = 5 - finishedGoalCount;
                storedRank = 1;
            }
            else if (finishedGoalCount == 0)
            {
                amountToNextRank = 1;
                storedRank = 0;
            }

            preferencesEditor.PutInt("Rank", storedRank);
            preferencesEditor.Apply();

            rankName.Text = rank;
            if (amountToNextRank != -1)
            {
                toNextRank.Text = "Keep completing goals!\n(" + amountToNextRank + " goal(s) to the next rank!)";
            }
            else
            {
                toNextRank.Text = "Congratulations!\nYou're at the top!\nKeep completing goals though!";
            }
        }
    }
}