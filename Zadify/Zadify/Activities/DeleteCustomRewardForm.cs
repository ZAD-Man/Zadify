using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Delete Reward")]
    public class DeleteCustomRewardForm : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DeleteCustomRewardForm);

            var layout = FindViewById<LinearLayout>(Resource.Id.DeleteCustomRewardFormLayout);
            layout.SetBackgroundResource(Resource.Color.darkblue);

            var confirmDeleteButton = FindViewById<Button>(Resource.Id.ConfirmDeleteCustomRewardButton);
            confirmDeleteButton.Click += delegate
                {
                    var position = Intent.GetIntExtra("Position", -1);
                    if (position != -1)
                    {
                        var storedRewards = JavaIO.LoadData<List<Reward>>(this, "Rewards.zad");
                        if (storedRewards != null)
                        {
                            var deleteReward = storedRewards[position];
                            storedRewards.Remove(deleteReward);
                            bool successfulSave = JavaIO.SaveData(this, "Rewards.zad", storedRewards);
                            if (successfulSave)
                            {
                                Toast.MakeText(this, "Reward Deleted", ToastLength.Long).Show();
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "Error deleting reward", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            Log.Error("DeleteCustomRewardScreen:loadError", "Rewards not loaded");
                        }
                    }
                    else
                    {
                        Log.Error("DeleteCustomRewardScreen:IntentError", "Position is -1, intent not found");
                    }
                };
        }
    }
}