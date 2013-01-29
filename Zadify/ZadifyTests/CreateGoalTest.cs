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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadify;

namespace ZadifyTests
{
    [TestClass]
    class CreateGoalTest
    {
        [TestMethod]
        public void CreateReadingByDateGoal()
        {
            var date = new DateTime(2013, 5, 12);
            var readingItems = ReadingItems.Words;
            var itemsGoalNumber = 108;
            var readingGoal = new ReadingGoal(date, itemsGoalNumber, readingItems);
            Assert.AreEqual(date, readingGoal.DueDate);
            Assert.AreEqual(ReadingItems.Words, readingGoal.MeasuredItems);
            Assert.AreEqual(108, readingGoal.GoalAmount);
            Assert.AreEqual(0, readingGoal.GoalCompletedAmount);
            Assert.AreEqual(0, readingGoal.Progress);
        }

        [TestMethod]
        public void UpdateReadingByDateGoalProgress()
        {
            var date = new DateTime(2013, 5, 12);
            var readingItems = ReadingItems.Words;
            var itemsGoalNumber = 108;
            var readingByDateGoal = new ReadingGoal(date, itemsGoalNumber, readingItems);
            readingByDateGoal.UpdateProgress(42);
            Assert.AreEqual(42, readingByDateGoal.GoalCompletedAmount);
            Assert.AreEqual((double)42/108, readingByDateGoal.Progress);
        }
    }
}