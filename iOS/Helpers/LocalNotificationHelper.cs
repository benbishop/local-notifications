using System;
using UIKit;

namespace RendrKit.LocalNotifications.iOS.Helpers
{
    public static class LocalNotificationHelper
    {
		public static Models.LocalNotification CreateFromUILocalNotification(UILocalNotification uiNotification)
		{
			var notification = new Models.LocalNotification();

			notification.Id = uiNotification.UserInfo.ValueForKey(new Foundation.NSString("id")).ToString();
            notification.FireDate = new DateTime(long.Parse(uiNotification.UserInfo.ValueForKey(new Foundation.NSString("fire_date")).ToString()));
			notification.Text = uiNotification.UserInfo.ValueForKey(new Foundation.NSString("text")).ToString();

			return notification;
		}
    }
}
