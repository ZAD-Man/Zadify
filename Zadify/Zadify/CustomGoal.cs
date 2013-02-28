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

namespace Zadify
{
    [Serializable]
    public class CustomGoal : Goal
    {
        public string MeasuredItems { get; private set; }

        public CustomGoal()
        {
        }

        public CustomGoal(DateTime dueDate, int goalAmount, string measuredItems, int repeatingDays = 0)
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
            var displayString = "Do " + GoalAmount + " " + MeasuredItems.ToLower() + " - " + (int)(Progress * 100) + "%";
            return displayString;
        }
    }
}