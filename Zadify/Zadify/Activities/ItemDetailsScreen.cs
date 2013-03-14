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
using Zadify.Enums;

namespace Zadify.Activities
{
    [Activity(Label = "My Activity")]
    public class ItemDetailsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ItemDetailsScreen);

            var layout = FindViewById<LinearLayout>(Resource.Id.ItemDetailsScreenLayout);
            layout.SetBackgroundResource(Resource.Color.darkblue);

            var itemTypeName = FindViewById<TextView>(Resource.Id.ItemTypeName);
            var foodNames = FindViewById<TextView>(Resource.Id.FoodNames);
            var defenseNames = FindViewById<TextView>(Resource.Id.DefenseNames);
            var weaponNames = FindViewById<TextView>(Resource.Id.WeaponNames);

            var itemPosition = Intent.GetIntExtra("Position", -1);

            switch (itemPosition)
            {
                case 0:
                    itemTypeName.Text = "Zombie";
                    foreach (var zombieFood in Enum.GetValues(typeof (ZombieFoods)))
                    {
                        foodNames.Text += zombieFood + ", ";
                    }
                    foreach (var zombieDefense in Enum.GetValues(typeof (ZombieDefenses)))
                    {
                        defenseNames.Text += zombieDefense + ", ";
                    }
                    foreach (var zombieWeapon in Enum.GetValues(typeof (ZombieWeapons)))
                    {
                        weaponNames.Text += zombieWeapon + ", ";
                    }
                    break;
                case 1:
                    itemTypeName.Text = "Skeleton";
                    foreach (var skeletonFood in Enum.GetValues(typeof (SkeletonFoods)))
                    {
                        foodNames.Text += skeletonFood + ", ";
                    }
                    foreach (var skeletonDefense in Enum.GetValues(typeof (SkeletonDefenses)))
                    {
                        defenseNames.Text += skeletonDefense + ", ";
                    }
                    foreach (var skeletonWeapon in Enum.GetValues(typeof (SkeletonWeapons)))
                    {
                        weaponNames.Text += skeletonWeapon + ", ";
                    }
                    break;
                default:
                    Log.Error("Item Details", "Invalid position received: " + itemPosition);
                    itemTypeName.Text = "Items not loaded";
                    foodNames.Text = "";
                    defenseNames.Text = "";
                    weaponNames.Text = "";
                    break;
            }

            foodNames.Text = foodNames.Text.TrimEnd(new[] {' ', ','});
            defenseNames.Text = defenseNames.Text.TrimEnd(new[] {' ', ','});
            weaponNames.Text = weaponNames.Text.TrimEnd(new[] {' ', ','});
        }
    }
}