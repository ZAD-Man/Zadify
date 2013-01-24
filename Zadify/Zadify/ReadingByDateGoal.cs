using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zadify
{
    [Serializable]
    public class ReadingByDateGoal : IReadingGoal
    {
        public ReadingByDateGoal()
        {
            
        }
        public ReadingByDateGoal(DateTime dueDate, ReadingItems readingItems, int itemsGoalNumber)
        {
            ItemsGoalNumber = itemsGoalNumber;
            ReadingItems = readingItems;
            DueDate = dueDate;
            UpdateProgress(0);
        }

        public double Progress { get; private set; }
        public DateTime DueDate { get; private set; }
        public ReadingItems ReadingItems { get; private set; }
        public int ItemsGoalNumber { get; private set; }
        public int ItemsCompletedNumber { get; private set; }

        public void UpdateProgress(int itemsRead)
        {
            ItemsCompletedNumber = itemsRead;
            Progress = (double) ItemsCompletedNumber/ItemsGoalNumber;
        }
    }
}