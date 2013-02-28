using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zadify.Activities
{
    [Activity(Label = "Monsters!")]
    public class MonsterDisplay : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("MonsterDisplay", "Monster Display Created");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MonsterDisplay);

            var monsterText = FindViewById<TextView>(Resource.Id.MonsterText);

            var displayType = Intent.GetStringExtra("DisplayType");
            var percentDone = Intent.GetIntExtra("PercentDone", -1);
            var monster = Intent.GetStringExtra("Monster");
            var food = Intent.GetStringExtra("Food");
            var defense = Intent.GetStringExtra("Defense");
            var weapon = Intent.GetStringExtra("Weapon");

            switch (displayType)
            {
                case "Create":
                    monsterText.Text = string.Format("You are locked in a room. You can hear a {0} trying to break in! Complete your goal to survive!", monster);
                    break;
                case "Nothing":
                    monsterText.Text = "You search around the room, but don't find anything useful.";
                    break;
                case "Progress":
                    if (percentDone >= 30 && percentDone < 60)
                    {
                        monsterText.Text = string.Format("You open an small ice box and find {0}! You were starting to get hungry anyway, so you quickly eat it.", food);
                    }
                    else if (percentDone >= 60 && percentDone < 90)
                    {
                        monsterText.Text = string.Format("You manage to push a heavy dresser off of a pile of clutter. Underneath it you find a {0}! This will be useful should worst come to worst.", defense);
                    }
                    else if (percentDone >= 90 && percentDone < 100)
                    {
                        monsterText.Text = string.Format("You find a safe in the far corner of the room. The door is cracked open, so you pull it open and look inside. There's a {0} in there! You eagerly take it. You feel a lot better with the {0} at your side.", weapon);
                    }
                    else if (percentDone >= 100)
                    {
                        monsterText.Text = string.Format("You set up debris in front of the door. You adjust your {0} and ready your {1}. When that {2} comes through the door, you'll be ready.", defense, weapon, monster);
                    }
                    break;
                case "Complete":
                    if (percentDone >= 0 && percentDone < 30)
                    {
                        monsterText.Text = string.Format("Things are looking bleak. You're tired, hungry, and haven't managed to find anything useful. Suddenly, the {0} bursts in! You barely manage to scream before it attacks you. It quickly takes you down, leaving your crumpled body on the groud as it searches for someone else. You are dead...", monster);
                    }
                    else if (percentDone >= 30 && percentDone < 60)
                    {
                        monsterText.Text = string.Format("You continue searching through the contents of the room, but suddenly the {0} bursts into the room! As it approches you to attack, you shove it away as hard as you can and run past it for the door. Unfortunately, you are greeted there by another {0}, which takes you out before you can react. You are dead...", monster);
                    }
                    else if (percentDone >= 60 && percentDone < 90)
                    {
                        monsterText.Text = string.Format("You've just finished adjusting your new {0} when the {1} bursts in! It attacks before you can react, but luckily the brunt of the force goes to your {0}. You retaliate as best you can, with a hard punch and a swift kick. Unfortunately, your kick is intercepted and your ankle broken. Though pain races through your leg, you manage to punch the {1} one last time before you fall over. The {1}, apparently oblivious to your predicament, retreats through the door, perhaps searching for easier prey. Your consciousness begins to slip away. Your last thought before you pass is, \"Now what...?\"", defense, monster);
                    }
                    else if (percentDone >= 90 && percentDone < 100)
                    {
                        monsterText.Text = string.Format("You return to the door, feeling much more confident with your new weapon. Just as you reach the area near the door, the {0} bursts through! It lunges at you, but it wasn't expected your {1}. You badly wound it, and it reels back in pain. However, before you can attack again, the {0} lunges again, this time low to ground, at your legs. It connects, and you tumble to the ground. Luckily, you manage to keep your {1}, and even from your back you manage to hurt it again. You stagger to your feet as quickly as you can, but by the time you're up, the {0} has retreated out the door. You breathe a sigh of relief. Though it got away, and may eventually return, at least you've survived this encounter.", monster, weapon);
                    }
                    else if (percentDone > 100)
                    {
                        monsterText.Text = string.Format("You grit your teeth as the door begins to give way to the {0}'s beatings. Suddenly, it bursts, and the {0} is in the room! It immediately lunges at you, but falls over a chair before it can come close. You quickly attack it with your {1}. Though it howls with pain, it grabs hold of the chair it fell over and swings it at you. You are grateful for your {2} as it turns a potentially deadly blow into a mere annoyance. Before the {0} can attack again, you retaliate with a quick, finishing blow. You watch it for a few moments to be sure it's dead, then fall to your knees as you relax. Though you can still hear your heart in your ears, you feel impressed with yourself for you victory, and ready to take on anything else that lies beyond that door. You ready your {1} again and move out.", monster, weapon, defense);
                    }
                    break;
            }

            var monsterOKButton = FindViewById<TextView>(Resource.Id.MonsterOKButton);
            monsterOKButton.Click += delegate { Finish(); };
        }
    }
}