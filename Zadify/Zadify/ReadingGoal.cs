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
    public class ReadingGoal : Goal
    {
        public ReadingItems MeasuredItems { get; private set; }

        public ReadingGoal()
        {
        }

        public ReadingGoal(DateTime dueDate, int goalAmount, ReadingItems measuredItems, int repeatingDays = 0)
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
            var displayString = "Read " + GoalAmount + " " + MeasuredItems.ToString().ToLower() + " - " + (int)(Progress * 100) + "%";
            return displayString;
        }
    }
}