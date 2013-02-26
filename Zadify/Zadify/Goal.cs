using System;
using System.Xml.Serialization;
using Zadify.Enums;

namespace Zadify
{
    [Serializable]
    [XmlInclude(typeof (DietGoal))]
    [XmlInclude(typeof (FinanceGoal))]
    [XmlInclude(typeof (FitnessGoal))]
    [XmlInclude(typeof (ReadingGoal))]
    [XmlInclude(typeof (WritingGoal))]
    [XmlInclude(typeof (CustomGoal))]
    public abstract class Goal
    {
        public DateTime DueDate { get; protected set; }
        public double Progress { get; protected set; }
        public int GoalAmount { get; protected set; }
        public int GoalCompletedAmount { get; protected set; }
        public int RepeatingDays { get; protected set; }
        public bool ViewedPostDueDate { get; private set; }
        public string Monster { get; private set; }
        public string Food { get; private set; }
        public string Defense { get; private set; }
        public string Weapon { get; private set; }

        public void UpdateProgress(int amountCompleted)
        {
            GoalCompletedAmount = amountCompleted;
            Progress = Math.Abs((double) GoalCompletedAmount/GoalAmount);
        }

        public void Viewed()
        {
            ViewedPostDueDate = true;
        }

        public void AssignMonsterData(int rank)
        {
            var random = new Random();

            var monsterTypeValues = Enum.GetValues(typeof (Monsters));
            var monster = (Monsters) monsterTypeValues.GetValue(random.Next(rank));

            Monster = monster.ToString();

            switch (monster)
            {
                case Monsters.Zombie:
                    var zombieFoodValues = Enum.GetValues(typeof (ZombieFoods));
                    var zombieDefenseValues = Enum.GetValues(typeof (ZombieDefenses));
                    var zombieWeaponValues = Enum.GetValues(typeof (ZombieWeapons));

                    var zombieFood = (ZombieFoods) zombieFoodValues.GetValue(random.Next(zombieFoodValues.Length));
                    var zombieDefense = (ZombieDefenses) zombieDefenseValues.GetValue(random.Next(zombieDefenseValues.Length));
                    var zombieWeapon = (ZombieWeapons) zombieWeaponValues.GetValue(random.Next(zombieWeaponValues.Length));

                    switch (zombieFood)
                    {
                        case ZombieFoods.Apple:
                        case ZombieFoods.Rations:
                            Food = zombieFood.ToString();
                            break;
                        case ZombieFoods.CornedBeef:
                            Food = "Corned Beef";
                            break;
                    }

                    switch (zombieDefense)
                    {
                        case ZombieDefenses.GarbageLid:
                            Defense = "Garbage Lid";
                            break;
                        case ZombieDefenses.PoliceUniform:
                            Defense = "Police Uniform";
                            break;
                    }

                    switch (zombieWeapon)
                    {
                        case ZombieWeapons.Handgun:
                        case ZombieWeapons.Shotgun:
                            Weapon = zombieWeapon.ToString();
                            break;
                        case ZombieWeapons.BoardAndNail:
                            Weapon = "Board and Nail";
                            break;
                    }
                    break;
                case Monsters.Skeleton:
                    var skeletonFoodValues = Enum.GetValues(typeof (SkeletonFoods));
                    var skeletonDefenseValues = Enum.GetValues(typeof (SkeletonDefenses));
                    var skeletonWeaponValues = Enum.GetValues(typeof (SkeletonWeapons));

                    var skeletonFood = (SkeletonFoods) skeletonFoodValues.GetValue(random.Next(skeletonFoodValues.Length));
                    var skeletonDefense = (SkeletonDefenses) skeletonDefenseValues.GetValue(random.Next(skeletonDefenseValues.Length));
                    var skeletonWeapon = (SkeletonWeapons) skeletonWeaponValues.GetValue(random.Next(skeletonWeaponValues.Length));

                    switch (skeletonFood)
                    {
                        case SkeletonFoods.Lutefisk:
                            Food = skeletonFood.ToString();
                            break;
                        case SkeletonFoods.FriedChicken:
                            Food = "Fried Chicken";
                            break;
                        case SkeletonFoods.DriedFish:
                            Food = "Dried Fish";
                            break;
                    }

                    switch (skeletonDefense)
                    {
                        case SkeletonDefenses.KnightArmor:
                            Defense = "Knight Armor";
                            break;
                        case SkeletonDefenses.MotorcyleHelmet:
                            Defense = "Motorcycle Helmet";
                            break;
                    }

                    switch (skeletonWeapon)
                    {
                        case SkeletonWeapons.Katana:
                        case SkeletonWeapons.Sledgehammer:
                            Weapon = skeletonWeapon.ToString();
                            break;
                        case SkeletonWeapons.BaseballBat:
                            Weapon = "Baseball Bat";
                            break;
                    }
                    break;
                case Monsters.Mummy:
                    //TODO: Fill in
                    break;
                case Monsters.Robot:
                    //TODO: Fill in
                    break;
                case Monsters.Demon:
                    //TODO: Fill in
                    break;
            }
        }
    }
}