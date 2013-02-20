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
        public FinanceGoal()
        {
            
        }

        public FinanceGoal(DateTime dueDate, int goalAmount)
            : this(dueDate, goalAmount, new TimeSpan())
        {
        }

        public FinanceGoal(DateTime dueDate, int goalAmount, TimeSpan repeatingTime)
        {
            RepeatingTime = repeatingTime;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            GoalCompletedAmount = 0;
            Progress = 0;
        }
    }
}