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
using Java.IO;

namespace Zadify
{
    interface IGoal
    {
        double Progress { get; }
        void UpdateProgress(int indicator);
    }
}