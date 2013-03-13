using System;
using PushSharp;
using PushSharp.Android;

class Server
{
    private static void Main()
    {
        Console.WriteLine("Server");

        //Create our service	
        var push = new PushService();

        //Wire up the events
        push.Events.OnDeviceSubscriptionExpired += Events_OnDeviceSubscriptionExpired;
        push.Events.OnDeviceSubscriptionIdChanged += Events_OnDeviceSubscriptionIdChanged;
        push.Events.OnChannelException += Events_OnChannelException;
        push.Events.OnNotificationSendFailure += Events_OnNotificationSendFailure;
        push.Events.OnNotificationSent += Events_OnNotificationSent;
        push.Events.OnChannelDestroyed += Events_OnChannelDestroyed;
        push.Events.OnChannelCreated += Events_OnChannelCreated;

        //Configure and start Android GCM
        push.StartGoogleCloudMessagingPushService(
          new GcmPushChannelSettings("563954891250", "AIzaSyAslG83ihwODoQwEeHe8Krn8FLbiXliZ6g", "com.pushsharp.test")); //Project ID + API Key, see https://code.google.com/apis/console/

        //Fluent construction of an Android GCM Notification
        push.QueueNotification(NotificationFactory.AndroidGcm()
          .ForDeviceRegistrationId("APA91bHBm8aVBgwmxYayv_jE4889E7FK_J9bl2K_QmY90WZe6Yt0ZYY0W6Z6wITYIY9DSXK9_kJWGxac3f-1sJhC3xJZgsjq2sKfHrC7SkFIg26R--iz2cSmg5i0d0yG6FV-NzIN9pUJ") //Retrieved when device was registered
          .WithCollapseKey("NONE")
          .WithJson("{\"alert\":\"One more ZAD Alert Text! (On ZADify??)\",\"badge\":\"7\"}"));

        Console.WriteLine("Waiting for Queue to Finish...");

        //Stop and wait for the queues to drains
        push.StopAllServices();
        
        Console.ReadLine();
    }

    static void Events_OnDeviceSubscriptionIdChanged(PushSharp.Common.PlatformType platform, string oldDeviceInfo, string newDeviceInfo, PushSharp.Common.Notification notification)
    {
        //Currently this event will only ever happen for Android GCM
        Console.WriteLine("Device Registration Changed:  Old-> " + oldDeviceInfo + "  New-> " + newDeviceInfo);
    }

    static void Events_OnNotificationSent(PushSharp.Common.Notification notification)
    {
        Console.WriteLine("Sent: " + notification.Platform + " -> " + notification);
    }

    static void Events_OnNotificationSendFailure(PushSharp.Common.Notification notification, Exception notificationFailureException)
    {
        Console.WriteLine("Failure: " + notification.Platform + " -> " + notificationFailureException.Message + " -> " + notification);
    }

    static void Events_OnChannelException(Exception exception, PushSharp.Common.PlatformType platformType, PushSharp.Common.Notification notification)
    {
        Console.WriteLine("Channel Exception: " + platformType + " -> " + exception);
    }

    static void Events_OnDeviceSubscriptionExpired(PushSharp.Common.PlatformType platform, string deviceInfo, PushSharp.Common.Notification notification)
    {
        Console.WriteLine("Device Subscription Expired: " + platform + " -> " + deviceInfo);
    }

    static void Events_OnChannelDestroyed(PushSharp.Common.PlatformType platformType, int newChannelCount)
    {
        Console.WriteLine("Channel Destroyed for: " + platformType + " Channel Count: " + newChannelCount);
    }

    static void Events_OnChannelCreated(PushSharp.Common.PlatformType platformType, int newChannelCount)
    {
        Console.WriteLine("Channel Created for: " + platformType + " Channel Count: " + newChannelCount);
    }
}