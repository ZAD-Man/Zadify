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
    public class Reward
    {
        public Reward()
        {
        }

        public Reward(string name, string content, List<Goal> requiredGoals)
        {
            Name = name;
            Content = content;
            RequiredGoals = requiredGoals;
        }

        public string Name { get; private set; }
        public string Content { get; private set; }
        public List<Goal> RequiredGoals { get; private set; }

        public void AddContent(string content)
        {
            Content = content;
        }

        public void SetGoals(List<Goal> requiredGoals)
        {
            RequiredGoals = requiredGoals;
        }

        public void AddGoal(Goal requiredGoal)
        {
            if (RequiredGoals == null)
            {
                RequiredGoals = new List<Goal>();
            }
            RequiredGoals.Add(requiredGoal);
        }

        public void UpdateGoals(List<Goal> storedGoals)
        {
            if (RequiredGoals != null)
            {
                for (int i = 0; i < RequiredGoals.Count; i++)
                {
                    RequiredGoals[i] = storedGoals[storedGoals.IndexOf(RequiredGoals[i])];
                }
            }
        }

        public bool IsUnlocked()
        {
            bool isUnlocked = true;
            if (RequiredGoals != null)
            {
                foreach (var requiredGoal in RequiredGoals)
                {
                    if (requiredGoal.GoalCompletedAmount < requiredGoal.GoalAmount)
                    {
                        isUnlocked = false;
                    }
                }
            }
            else
            {
                isUnlocked = false;
            }
            return isUnlocked;
        }
    }
}