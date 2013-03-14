using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "My Activity")]
    public class SelectCustomRewardGoal : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");

            ListView.SetBackgroundResource(Resource.Color.darkblue);

            if (storedGoals != null)
            {
                var selectGoalStrings = (from storedGoal in storedGoals where !storedGoal.IsPastDue() select storedGoal.Summary()).ToList();
                ListAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, selectGoalStrings);
            }
            else
            {
                Log.Error("SelectCustomRewardGoal:", "Error loading goals");
                Toast.MakeText(this, "Problem loading goals", ToastLength.Long).Show();
            }
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);
            var preferencesEditor = preferences.Edit();
            preferencesEditor.PutInt("SelectedGoalPosition", position);
            preferencesEditor.Apply();
            Finish();
        }
    }
}