## RendrKit.LocalNotifications for Xamarin Droid and iOS

Schedule local notifications using shared code for your Droid and iOS apps

## Nuget

* [RendrKit.LocalNotifications](https://www.nuget.org/packages/RendrKit.LocalNotifications) [![NuGet](https://img.shields.io/nuget/v/RendrKit.LocalNotifications.svg?label=NuGet)](https://www.nuget.org/packages/RendrKit.LocalNotifications)

## Build

* [![Build status](https://ci.appveyor.com/api/projects/status/ryc8is0njixbuq9n?svg=true)](https://ci.appveyor.com/project/pauloortins/local-notifications)
* CI Nuget Feed: https://ci.appveyor.com/nuget/local-notifications-ds01nu97fcao

## Getting Started

```
PM> Install-Package RendrKit.LocalNotifications -Version 1.0.0
```

Install on all your projects including your client projects.
Use an IoC container to register the ILocalNotificationService to LocalNotificationService on your client projects or create a instance manually.

```csharp
Mvx.LazyConstructAndRegisterSingleton<ILocalNotificationService, LocalNotificationService>();
```

```csharp
App.NotificationService = new LocalNotificationService();
```

## Using API

```csharp
public interface ILocalNotificationService
{
    /*
     *   Add a notification with Text and FireDate, Id is set with a random integer
     */
    LocalNotification AddNotification(LocalNotification notification); 
    
    /*
     *    Remove notification using an Id 
     */
    void RemoveNotification(int notificationId);
}
```

## iOS

To receive the local notification on iOS, we should change register for local notifications and override the ReceivedLocalNotification method. We have a helper to convert the UINotification to our LocalNotification object.

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
  global::Xamarin.Forms.Forms.Init();

  LoadApplication(new App());
  
  var version = new Version(UIDevice.CurrentDevice.SystemVersion);
	if (version.Major > 8)
	{
	  var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
    app.RegisterUserNotificationSettings(settings);
  }            

  return base.FinishedLaunching(app, options);
}
```

```csharp
public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
{
  var localNotification = LocalNotificationHelper.CreateFromUILocalNotification(notification);

  var alert = new UIAlertView("LocalNotification", localNotification.Text, null, "Ok");
  alert.Show();
}
```

## Android

On Droid, there is not need to add configs on the MainActivity, but different from iOS (where you need to show messages manually), on Droid it's handled by the Receivers/Service and we can customize it. There is three ways we can customize the alert dialogs/notifications.

### Customizing Title/Icon

```csharp
// MainActivity.cs

// This will set the title and icon to your alert dialog (if app is on foreground) or the title and icon for the notification (if app is on background/closed)

LocalNotificationsAndroid.IconResource = Resource.Drawable.ic_media_play;
LocalNotificationsAndroid.Title = "Testing";
```

### Setting a custom dialog

```csharp
LocalNotificationsAndroid.ShowMessage = (Context context, LocalNotifications.Models.LocalNotification localNotification) => 
{
  var alertDialog = new AlertDialog.Builder(context)
  .SetTitle("Custom Show Message Title")
  .SetMessage("Custom Show Message Text")
  .SetCancelable(false)
  .SetPositiveButton("Ok", (sender, e) => { });
  
  alertDialog.Show();
};
```

### Setting a custom notification

```csharp
LocalNotificationsAndroid.ShowNotification = (Context context, LocalNotifications.Models.LocalNotification localNotification) => 
{
  Notification.Builder builder = new Notification.Builder(ApplicationContext)
  .SetContentTitle("Custom Show Notification Title")
  .SetAutoCancel(true)
  .SetSmallIcon(Resource.Drawable.ic_media_play)
  .SetContentText("Custom Show Notification Text");

  Intent resultIntent = new Intent(this, typeof(MainActivity));				
  resultIntent.SetFlags(ActivityFlags.PreviousIsTop);

  PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.UpdateCurrent);

  builder.SetContentIntent(resultPendingIntent);

  ((NotificationManager)context.GetSystemService(Android.Content.Context.NotificationService)).Notify(localNotification.Id, builder.Build());
};
```
