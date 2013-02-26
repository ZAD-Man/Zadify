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
    public class WritingGoal : Goal
    {
        public WritingItems MeasuredItems { get; private set; }

        public WritingGoal()
        {
        }

        public WritingGoal(DateTime dueDate, int goalAmount, WritingItems measuredItems, int repeatingDays = 0)
        {
            RepeatingDays = repeatingDays;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            MeasuredItems = measuredItems;
            GoalCompletedAmount = 0;
            Progress = 0;
        }
    }
}