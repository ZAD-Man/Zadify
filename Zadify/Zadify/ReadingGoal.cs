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
    public class ReadingGoal : Goal
    {
        public ReadingItems MeasuredItems { get; private set; }

        public ReadingGoal()
        {
            
        }

        public ReadingGoal(DateTime dueDate, int goalAmount, ReadingItems measuredItems)
            : this(dueDate, goalAmount, measuredItems, new TimeSpan())
        {
        }

        public ReadingGoal(DateTime dueDate, int goalAmount, ReadingItems measuredItems, TimeSpan repeatingTime)
        {
            RepeatingTime = repeatingTime;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            MeasuredItems = measuredItems;
            GoalCompletedAmount = 0;
            Progress = 0;
        }
    }
}