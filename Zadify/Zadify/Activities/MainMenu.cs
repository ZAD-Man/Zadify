using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Zadify.Enums;

namespace Zadify.Activities
{
    [Activity(Label = "Zadify", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("MainMenu", "Main Menu Created");
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainMenu);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);
            var preferencesEditor = preferences.Edit();
            
            if (!preferences.Contains("Rank"))
            {
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            if (!preferences.Contains("MonsterMode"))
            {
                preferencesEditor.PutBoolean("MonsterMode", true);
                preferencesEditor.Apply();
            }

            var rank = preferences.GetInt("Rank", -1);

            try
            {
                var goalsList = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                if (goalsList == null)
                {
                    goalsList = new List<Goal>();
                    JavaIO.SaveData(this, "Goals.zad", goalsList);
                }
            }
            catch (FileNotFoundException)
            {
                var goalsList = new List<Goal>();
                JavaIO.SaveData(this, "Goals.zad", goalsList);
            }

            try
            {
                var rewardsList = JavaIO.LoadData<List<Goal>>(this, "Rewards.zad");
                if (rewardsList == null)
                {
                    rewardsList = new List<Goal>();
                    JavaIO.SaveData(this, "Rewards.zad", rewardsList);
                }
            }
            catch (FileNotFoundException)
            {
                var rewardsList = new List<Goal>();
                JavaIO.SaveData(this, "Rewards.zad", rewardsList);
            }

            var goalsButton = FindViewById<Button>(Resource.Id.GoalsButton);
            goalsButton.Click += delegate { StartActivity(typeof (GoalsMenu)); };

            var rewardsButton = FindViewById<Button>(Resource.Id.RewardsButton);
            rewardsButton.Click += delegate { StartActivity(typeof (RewardsMenu)); };

            var socialButton = FindViewById<Button>(Resource.Id.SocialButton);
//            socialButton.Click += delegate { StartActivity(typeof (SocialMenu)); };
            socialButton.Visibility = ViewStates.Gone;

            var settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            settingsButton.Click += delegate { StartActivity(typeof (SettingsMenu)); };

            var setupButton = FindViewById<Button>(Resource.Id.SetupButton);
            setupButton.Click += delegate
                {
                    var idesOfMarch2020 = new DateTime(2020, 3, 15);
                    var fitnessGoal = new FitnessGoal(DateTime.Today, 23, FitnessItems.Pullups, 7);
                    var dietGoal = new DietGoal(idesOfMarch2020, -15, DietItems.Pounds, 7);
                    var customGoal = new CustomGoal(DateTime.Today, 8, "good deeds", 8);
                    var futureCustomGoal = new CustomGoal(idesOfMarch2020, 42, "paintings");

                    fitnessGoal.AssignMonsterData(rank);
                    dietGoal.AssignMonsterData(rank);
                    customGoal.AssignMonsterData(rank);
                    futureCustomGoal.AssignMonsterData(rank);

                    customGoal.UpdateProgress(15);

                    var goalsList = new List<Goal> {fitnessGoal, dietGoal, customGoal, futureCustomGoal};
                    var successfulGoalSave = JavaIO.SaveData(this, "Goals.zad", goalsList);

                    var completeLongDietReward = new Reward("Long Diet", "You are awesome", new List<Goal> {dietGoal});
                    var doGoodDeedsReward = new Reward("Good Deeds Game", "Psychonauts: 5e728-vvd79-6hwx2", new List<Goal> {customGoal});

                    var rewardsList = new List<Reward> {completeLongDietReward, doGoodDeedsReward};
                    var successfulRewardSave = JavaIO.SaveData(this, "Rewards.zad", rewardsList);

                    if (successfulRewardSave && successfulGoalSave)
                    {
                        Toast.MakeText(this, "Setup Complete", ToastLength.Short).Show();
                    }
                    else if (!successfulGoalSave)
                    {
                        Toast.MakeText(this, "Goal Setup Failed", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Reward Setup Failed", ToastLength.Short).Show();
                    }
                };

            var monsterDemoButton = FindViewById<Button>(Resource.Id.MonsterDemoButton);
//            monsterDemoButton.Click += delegate { StartActivity(typeof (MonsterDisplay)); };
            monsterDemoButton.Visibility = ViewStates.Gone;

            var pushActivityButton = FindViewById<Button>(Resource.Id.PushActivityButton);
            pushActivityButton.Click += delegate { StartActivity(typeof (PushActivity)); };
            pushActivityButton.Visibility = ViewStates.Gone;
        }
    }
}