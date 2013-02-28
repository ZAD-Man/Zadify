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
    [Activity(Label = "Custom Rewards")]
    public class CustomRewardsMenu : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("CustomRewardsMenu", "Custom Rewards Menu Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CustomRewardsMenu);

            var createRewardButton = FindViewById<Button>(Resource.Id.CreateRewardButton);
            createRewardButton.Click += delegate { StartActivity(typeof (CreateRewardForm)); };
            try
            {
                var rewardsList = FindViewById<ListView>(Resource.Id.CustomRewardsList);
                var storedRewardStrings = new List<string>();
                var storedRewardList = JavaIO.LoadData<List<Reward>>(this, "Rewards.zad");
                if (storedRewardList != null)
                {
                    storedRewardStrings.AddRange(storedRewardList.Select(reward => reward.Name));

                    var rewardsAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, storedRewardStrings);
                    rewardsList.Adapter = rewardsAdapter;

                    rewardsList.ItemClick += (sender, args) =>
                    {
                        var position = args.Position;
                        var customRewardDetailsScreen = new Intent(this, typeof(CustomRewardDetailsScreen));
                        customRewardDetailsScreen.PutExtra("Position", position);
                        StartActivity(customRewardDetailsScreen);
                    };
                }
            }
            catch (Java.IO.FileNotFoundException e)
            {
                Log.Error("CustomRewardsMenu:FileNotFound", e.Message + e.StackTrace);
                Toast.MakeText(this, "No rewards to display", ToastLength.Long).Show();
            }
            catch (Exception e)
            {
                Log.Error("CustomRewardsMenu:GeneralException", e.Message + e.StackTrace);
                Toast.MakeText(this, "Rewards could not be displayed", ToastLength.Long).Show();
            }

            var customRewardsList = FindViewById<ListView>(Resource.Id.CustomRewardsList);
        }
    }
}