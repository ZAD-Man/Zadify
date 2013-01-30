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
    public class FinanceGoal : Goal
    {
        public FinanceItems MeasuredItems { get; private set; }

        public FinanceGoal()
        {
            
        }

        public FinanceGoal(DateTime dueDate, int goalAmount, FinanceItems measuredItems)
            : this(dueDate, goalAmount, measuredItems, new TimeSpan())
        {
        }

        public FinanceGoal(DateTime dueDate, int goalAmount, FinanceItems measuredItems, TimeSpan repeatingTime)
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