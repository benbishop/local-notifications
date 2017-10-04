using System;
using System.Diagnostics;
using Foundation;
using RendrKit.LocalNotifications.Interfaces;
using RendrKit.LocalNotifications.Models;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace RendrKit.LocalNotifications.iOS.Implementations
{
    public class LocalNotificationService : ILocalNotificationService
    {
        public LocalNotification AddNotification(LocalNotification notification)
        {
			var date = new DateTime(notification.FireDate.Year, notification.FireDate.Month, notification.FireDate.Day);

			date = date.AddHours(notification.FireDate.Hour);
			date = date.AddMinutes(notification.FireDate.Minute);

			UILocalNotification uiNotification = new UILocalNotification
			{
				FireDate = date.ToNSDate(),
				TimeZone = NSTimeZone.LocalTimeZone,
				AlertBody = notification.Text,
				RepeatInterval = 0,				
				SoundName = UILocalNotification.DefaultSoundName,
				ApplicationIconBadgeNumber = 1
			};
            			
            Debug.WriteLine($"RendrKit.LocalNotifications: ADDED LOCAL NOTIFICATION: FireDate: {notification.FireDate} Message: {notification.Text}");
			UIApplication.SharedApplication.ScheduleLocalNotification(uiNotification);

            return notification;
        }
    }
}
