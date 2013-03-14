using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Unlocked Items")]
    public class UnlockedItemsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("UnlockedItemsScreen", "Unlocked Items Screen created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UnlockedItemsScreen);

            var layout = FindViewById<LinearLayout>(Resource.Id.UnlockedItemsScreenLayout);
            layout.SetBackgroundResource(Resource.Color.darkblue);

            var preferences = GetSharedPreferences("Preferences.zad", FileCreationMode.Private);
            var preferencesEditor = preferences.Edit();

            if (!preferences.Contains("Rank"))
            {
                preferencesEditor.PutInt("Rank", 0);
                preferencesEditor.Apply();
            }

            var storedRank = preferences.GetInt("Rank", -1);

            var unlockedItemsList = FindViewById<ListView>(Resource.Id.UnlockedItemsList);

            var itemsTypeStrings = new List<string> {"Zombie"};

            if (storedRank >= 1)
            {
                itemsTypeStrings.Add("Skeleton");
            }
            if (storedRank >= 2)
            {
                itemsTypeStrings.Add("Mummy");
            }
            if (storedRank >= 3)
            {
                itemsTypeStrings.Add("Robot");
            }
            if (storedRank >= 4)
            {
                itemsTypeStrings.Add("Demon");
            }

            var unlockedItemsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, itemsTypeStrings);

            unlockedItemsList.Adapter = unlockedItemsAdapter;

            unlockedItemsList.ItemClick += (sender, args) =>
                {
                    var position = args.Position;
                    var itemDetailsScreen = new Intent(this, typeof (ItemDetailsScreen));
                    itemDetailsScreen.PutExtra("Position", position);
                    StartActivity(itemDetailsScreen);
                };
        }
    }
}