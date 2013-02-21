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
    [XmlInclude(typeof(CustomGoal))]
    public abstract class Goal
    {
        public DateTime DueDate { get; protected set; }
        public double Progress { get; protected set; }
        public int GoalAmount { get; protected set; }
        public int GoalCompletedAmount { get; protected set; }
        public int RepeatingDays { get; protected set; }
        public bool ViewedPostDueDate { get; protected set; }

        public void UpdateProgress(int amountCompleted)
        {
            GoalCompletedAmount = amountCompleted;
            Progress = Math.Abs((double)GoalCompletedAmount / GoalAmount);
        }

        public void Viewed()
        {
            ViewedPostDueDate = true;
        }
    }
}