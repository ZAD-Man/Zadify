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
            var readingByDateGoal = new ReadingByDateGoal(date,readingItems, itemsGoalNumber);
            Assert.AreEqual(date, readingByDateGoal.DueDate);
            Assert.AreEqual(ReadingItems.Words, readingByDateGoal.ReadingItems);
            Assert.AreEqual(108, readingByDateGoal.ItemsGoalNumber);
            Assert.AreEqual(0, readingByDateGoal.ItemsCompletedNumber);
            Assert.AreEqual(0, readingByDateGoal.Progress);
        }

        [TestMethod]
        public void UpdateReadingByDateGoal()
        {
            var date = new DateTime(2013, 5, 12);
            var readingItems = ReadingItems.Words;
            var itemsGoalNumber = 108;
            var readingByDateGoal = new ReadingByDateGoal(date, readingItems, itemsGoalNumber);
            readingByDateGoal.UpdateProgress(42);
            Assert.AreEqual(42, readingByDateGoal.ItemsCompletedNumber);
            Assert.AreEqual((double)42/108, readingByDateGoal.Progress);
        }
    }
}