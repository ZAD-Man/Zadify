using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace Zadify
{
    [Serializable]
    [XmlInclude(typeof(DietGoal))]
    [XmlInclude(typeof(FinanceGoal))]
    [XmlInclude(typeof(FitnessGoal))]
    [XmlInclude(typeof(ReadingGoal))]
    [XmlInclude(typeof(WritingGoal))]
    public abstract class Goal
    {
        public DateTime DueDate { get; protected set; }
        public double Progress { get; protected set; }
        public int GoalAmount { get; protected set; }
        public int GoalCompletedAmount { get; protected set; }
        public TimeSpan RepeatingTime { get; protected set; }

        public void UpdateProgress(int amountCompleted)
        {
            GoalCompletedAmount = amountCompleted;
            Progress = (double)GoalCompletedAmount / GoalAmount;
        }
    }
}