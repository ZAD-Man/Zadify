using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    //TODO: Make this into a pop-up confirmation instead?
    [Activity(Label = "Delete Goal")]
    public class DeleteGoalForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("Delete Goal Form", "Delete Goal Form Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DeleteGoalForm);

            var confirmDeleteButton = FindViewById<Button>(Resource.Id.ConfirmDeleteGoalButton);
            confirmDeleteButton.Click += delegate
                {
                    var position = Intent.GetIntExtra("Position", -1);
                    if (position != -1)
                    {
                        var storedGoals = JavaIO.LoadData<List<Goal>>(this, "Goals.zad");
                        if (storedGoals != null)
                        {
                            var deleteGoal = storedGoals[position];
                            storedGoals.Remove(deleteGoal);
                            bool successfulSave = JavaIO.SaveData(this, "Goals.zad", storedGoals);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Goal Deleted", ToastLength.Long).Show();
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Error updating goal", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            Log.Error("GoalDetailsScreen:loadError", "Goals not loaded");
                        }
                    }
                    else
                    {
                        Log.Error("GoalDetailsScreen:IntentError", "Position is -1, intent not found");
                    }
                };
        }
    }
}