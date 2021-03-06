using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("Settings", "Settings Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SettingsMenu);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);

            var monsterModeCheckbox = FindViewById<CheckBox>(Resource.Id.MonsterModeCheckbox);
            monsterModeCheckbox.Checked = preferences.GetBoolean("MonsterMode", true);

            var saveSettingsButton = FindViewById<Button>(Resource.Id.SaveSettingsButton);
            saveSettingsButton.Click += delegate
                {
                    var preferencesEditor = preferences.Edit();
                    preferencesEditor.PutBoolean("MonsterMode", monsterModeCheckbox.Checked);
                    preferencesEditor.Apply();
                    Toast.MakeText(this, "Settings Saved", ToastLength.Long).Show();
                    Finish();
                };
        }
    }
}