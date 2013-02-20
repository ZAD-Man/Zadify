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
    public class FitnessGoal : Goal
    {
        public FitnessItems MeasuredItems { get; private set; }

        public FitnessGoal()
        {
            
        }

        public FitnessGoal(DateTime dueDate, int goalAmount, FitnessItems measuredItems, int repeatingDays = 0)
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