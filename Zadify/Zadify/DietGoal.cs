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
    public class DietGoal : Goal
    {
        public DietItems MeasuredItems { get; private set; }

        public DietGoal()
        {
            
        }

        public DietGoal(DateTime dueDate, int goalAmount, DietItems measuredItems)
            : this(dueDate, goalAmount, measuredItems, new TimeSpan())
        {
        }

        public DietGoal(DateTime dueDate, int goalAmount, DietItems measuredItems, TimeSpan repeatingTime)
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