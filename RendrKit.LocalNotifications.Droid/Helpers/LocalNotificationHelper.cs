using System;
using Android.Content;

namespace RendrKit.LocalNotifications.Droid.Helpers
{
    public static class LocalNotificationHelper
    {
        public static Models.LocalNotification CreateFromIntent(Intent intent)
        {
            var notification = new Models.LocalNotification();

            notification.Id = intent.GetIntExtra("id", 0);
            notification.FireDate = new DateTime(long.Parse(intent.GetStringExtra("fire_date")));
            notification.Text = intent.GetStringExtra("text");

            return notification;
        }
    }
}
