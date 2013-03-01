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
                    storedGoalStrings.AddRange(storedGoalList.Where(goal => goal.ViewedPostDueDate).Select(goal => goal.Summary()));

                    var completedGoalsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, storedGoalStrings);
                    completedGoalsList.Adapter = completedGoalsAdapter;

                    completedGoalsList.ItemClick += (sender, args) =>
                        {
                            var goalDetailsScreen = new Intent(this, typeof (GoalDetailsScreen));
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