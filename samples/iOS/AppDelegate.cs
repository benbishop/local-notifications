using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using RendrKit.LocalNotifications.iOS.Helpers;
using RendrKit.LocalNotifications.iOS.Implementations;
using UIKit;

namespace RendrKit.LocalNotification.Samples.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
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

            App.NotificationService = new LocalNotificationService();

            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            var localNotification = LocalNotificationHelper.CreateFromUILocalNotification(notification);

			var alert = new UIAlertView("LocalNotification", localNotification.Text, null, "Ok");
			alert.Show();
        }
    }
}
