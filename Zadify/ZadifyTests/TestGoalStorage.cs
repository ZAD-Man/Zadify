using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadify;
using Zadify.Enums;

namespace ZadifyTests
{
    [TestClass]
    class TestGoalStorage
    {
        [TestMethod]
        public void TestGoalListSaveAndLoad()
        {
            var date = new DateTime(2013, 5, 12);
            var readingItems = ReadingItems.Words;
            var itemsGoalNumber = 108;
            var readingGoal = new ReadingGoal(date, itemsGoalNumber, readingItems);
            var goalList = new List<Goal> {readingGoal};
            JavaIO.SaveData(Application.Context, "TestGoals.zad", goalList);
            var loadedGoalList = JavaIO.LoadData<List<Goal>>(Application.Context, "TestGoals.zad");
            Assert.IsNotNull(loadedGoalList);
            var loadedReadingGoal = (ReadingGoal) loadedGoalList[0];
            Assert.AreEqual(readingGoal.DueDate, loadedReadingGoal.DueDate);
            Assert.AreEqual(readingGoal.GoalAmount, loadedReadingGoal.GoalAmount);
            Assert.AreEqual(readingGoal.GoalCompletedAmount, loadedReadingGoal.GoalCompletedAmount);
            Assert.AreEqual(readingGoal.MeasuredItems, loadedReadingGoal.MeasuredItems);
            Assert.AreEqual(readingGoal.Progress, loadedReadingGoal.Progress);
            Assert.AreEqual(readingGoal.RepeatingDays, loadedReadingGoal.RepeatingDays);
        }

        [TestMethod]
        public void TestUpdateGoalListSaveAndLoad()
        {
            var date = new DateTime(2013, 5, 12);
            var readingItems = ReadingItems.Words;
            var itemsGoalNumber = 108;
            var readingGoal = new ReadingGoal(date, itemsGoalNumber, readingItems);
            var goalList = new List<Goal> { readingGoal };
            JavaIO.SaveData(Application.Context, "TestGoals.zad", goalList);
            var loadedGoalList = JavaIO.LoadData<List<Goal>>(Application.Context, "TestGoals.zad");
            var loadedReadingGoal = (ReadingGoal)loadedGoalList[0];
            readingGoal.UpdateProgress(42);
            Assert.AreNotEqual(readingGoal.GoalCompletedAmount, loadedReadingGoal.GoalCompletedAmount);
            Assert.AreNotEqual(readingGoal.Progress, loadedReadingGoal.Progress);
        }
    }
}
