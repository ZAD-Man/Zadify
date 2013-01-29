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
    public class FitnessGoal : IGoal
    {
        public FitnessGoal(DateTime dueDate, int goalAmount, FitnessItems measuredItems)
            : this(dueDate, goalAmount, measuredItems, new TimeSpan())
        {
        }

        public FitnessGoal(DateTime dueDate, int goalAmount, FitnessItems measuredItems, TimeSpan repeatingTime)
        {
            RepeatingTime = repeatingTime;
            GoalAmount = goalAmount;
            DueDate = dueDate;
            MeasuredItems = measuredItems;
            GoalCompletedAmount = 0;
            Progress = 0;
        }

        public DateTime DueDate { get; private set; }
        public double Progress { get; private set; }
        public int GoalAmount { get; private set; }
        public int GoalCompletedAmount { get; private set; }
        public FitnessItems MeasuredItems { get; private set; }
        public TimeSpan RepeatingTime { get; private set; }

        public void UpdateProgress(int amountCompleted)
        {
            GoalCompletedAmount = amountCompleted;
            Progress = (double)GoalCompletedAmount / GoalAmount;
        }
    }
}