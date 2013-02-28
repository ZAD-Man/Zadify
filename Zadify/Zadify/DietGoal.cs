using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Zadify.Enums;

namespace Zadify
{
    [Serializable]
    public class DietGoal : Goal
    {
        public DietItems MeasuredItems { get; private set; }

        public DietGoal()
        {
        }

        public DietGoal(DateTime dueDate, int goalAmount, DietItems measuredItems, int repeatingDays = 0)
        {
            RepeatingDays = repeatingDays;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            MeasuredItems = measuredItems;
            GoalCompletedAmount = 0;
            Progress = 0;
        }

        public override string Summary()
        {
            string displayString;
            if (GoalAmount > 0)
            {
                displayString = "Gain " + GoalAmount + " " + MeasuredItems.ToString().ToLower() + " - " + (int)(Progress * 100) + "%";
            }
            else
            {
                displayString = "Lose " + Math.Abs(GoalAmount) + " " + MeasuredItems.ToString().ToLower() + " - " + (int)(Progress * 100) + "%";
            }

            return displayString;
        }
    }
}