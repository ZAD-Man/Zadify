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

        public FinanceGoal(DateTime dueDate, int goalAmount, int repeatingDays = 0)
        {
            RepeatingDays = repeatingDays;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            GoalCompletedAmount = 0;
            Progress = 0;
        }
    }
}