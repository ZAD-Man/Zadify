using Android.App;
using Android.Content;
using PushSharp.Client;

namespace Zadify.Push
{
    [BroadcastReceiver(Permission = GCMConstants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { GCMConstants.INTENT_FROM_GCM_MESSAGE }, Categories = new[] { "zadify.app" })]
    [IntentFilter(new[] { GCMConstants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new[] { "zadify.app" })]
    [IntentFilter(new[] { GCMConstants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new[] { "zadify.app" })]
    public class PushHandlerBroadcastReceiver : PushHandlerBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { "563954891250" }; //My sender ID from GCM Console

        public const string TAG = "PushSharp-GCM";
    }
}