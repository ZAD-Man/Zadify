using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
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

            var preferencesEditor2 = preferences.Edit();
            preferencesEditor2.PutInt("Rank", 0);
            preferencesEditor2.Apply();

            if (!preferences.Contains("Rank"))
            {
                var preferencesEditor = preferences.Edit();
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            if (!preferences.Contains("MonsterMode"))
            {
                var preferencesEditor = preferences.Edit();
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

            var goalsButton = FindViewById<Button>(Resource.Id.GoalsButton);
            goalsButton.Click += delegate { StartActivity(typeof (GoalsMenu)); };

            var rewardsButton = FindViewById<Button>(Resource.Id.RewardsButton);
            rewardsButton.Click += delegate { StartActivity(typeof (RewardsMenu)); };

            var socialButton = FindViewById<Button>(Resource.Id.SocialButton);
            socialButton.Click += delegate { StartActivity(typeof (SocialMenu)); };

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
                    JavaIO.SaveData(this, "Goals.zad", goalsList);

                    Toast.MakeText(this, "Setup Complete", ToastLength.Short).Show();
                };

            var monsterDemoButton = FindViewById<Button>(Resource.Id.MonsterDemoButton);
            monsterDemoButton.Click += delegate { StartActivity(typeof (MonsterDisplay)); };
        }
    }
}