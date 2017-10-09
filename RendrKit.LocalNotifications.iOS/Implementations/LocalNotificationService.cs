using System;
using System.Diagnostics;
using Foundation;
using RendrKit.LocalNotifications.Interfaces;
using RendrKit.LocalNotifications.iOS.Extensions;
using RendrKit.LocalNotifications.Models;
using UIKit;

namespace RendrKit.LocalNotifications.iOS.Implementations
{
    public class LocalNotificationService : ILocalNotificationService
    {
        public LocalNotification AddNotification(LocalNotification notification)
        {
			var date = new DateTime(notification.FireDate.Year, notification.FireDate.Month, notification.FireDate.Day);

			date = date.AddHours(notification.FireDate.Hour);
			date = date.AddMinutes(notification.FireDate.Minute);

			var keys = new object[] { "id", "fire_date", "text" };
			var objects = new object[] { notification.Id, notification.FireDate.Ticks.ToString(), notification.Text ?? string.Empty };

			UILocalNotification uiNotification = new UILocalNotification
			{
				FireDate = date.ToNSDate(),
				TimeZone = NSTimeZone.LocalTimeZone,
				AlertBody = notification.Text,
                UserInfo = NSDictionary.FromObjectsAndKeys(objects, keys),
				RepeatInterval = 0,				
				SoundName = UILocalNotification.DefaultSoundName,
				ApplicationIconBadgeNumber = 1
			};
            			
            Debug.WriteLine($"RendrKit.LocalNotifications: ADDED LOCAL NOTIFICATION: Id: {notification.Id} FireDate: {notification.FireDate} Message: {notification.Text}");
			UIApplication.SharedApplication.ScheduleLocalNotification(uiNotification);

            return notification;
        }

        public void RemoveNotification(int notificationId)
        {
			foreach (UILocalNotification notification in UIApplication.SharedApplication.ScheduledLocalNotifications)
			{
                var id = int.Parse(notification.UserInfo.ValueForKey(new Foundation.NSString("id")).ToString());
                if (id == notificationId)
                {
                    UIApplication.SharedApplication.CancelLocalNotification(notification);
                    Debug.WriteLine($"RendrKit.LocalNotifications: REMOVED LOCAL NOTIFICATION: Id: {notificationId}");
                    break;
                }
			}
        }
    }
}
