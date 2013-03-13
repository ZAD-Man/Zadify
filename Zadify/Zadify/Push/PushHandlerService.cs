using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using PushSharp.Client;
using Zadify.Activities;

[assembly: Permission(Name = "zadify.app.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "zadify.app.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace Zadify.Push
{
    [Service]
    public class PushHandlerService : PushHandlerServiceBase
    {
        public PushHandlerService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Registered: " + registrationId);

            CreateNotification("PushSharp-GCM Registered...", "The device has been Registered, Tap to View!");
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

            CreateNotification("PushSharp-GCM Unregistered...", "The device has been unregistered, Tap to View!");
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(PushHandlerBroadcastReceiver.TAG, "GCM Message Received!");

            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));
            }

            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            CreateNotification("PushSharp-GCM Msg Rec'd", "Message Received for C2DM-Sharp... Tap to View!");
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(PushHandlerBroadcastReceiver.TAG, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(PushHandlerBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }

        void CreateNotification(string title, string desc)
        {
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;

            var uiIntent = new Intent(this, typeof(PushActivity));

            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title) {Flags = NotificationFlags.AutoCancel};

            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            if (notificationManager != null) notificationManager.Notify(1, notification);
        }
    }
}

