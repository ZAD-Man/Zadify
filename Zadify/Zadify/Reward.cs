using System;
using System.Collections.Generic;
using System.Linq;

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
                    foreach (Goal goal in storedGoals)
                    {
                        if (RequiredGoals[i].Summary() == goal.Summary())
                        {
                            RequiredGoals[i] = goal;
                        }
                    }
                }
            }
        }

        public bool IsUnlocked()
        {
            bool isUnlocked = true;
            if (RequiredGoals != null)
            {
                foreach (var requiredGoal in RequiredGoals.Where(requiredGoal => !requiredGoal.IsCompleted()))
                {
                    isUnlocked = false;
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