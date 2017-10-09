using System;
using Android.Content;

namespace RendrKit.LocalNotifications.Droid
{
    public static class LocalNotificationsAndroid
    {
        static string _title;
        public static string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        static int? _iconResource;
        public static int? IconResource
        {
            get { return _iconResource; }
            set { _iconResource = value; }
        }

        static Action<Context, LocalNotifications.Models.LocalNotification> _showMessage;
        public static Action<Context, LocalNotifications.Models.LocalNotification> ShowMessage
        {
            get { return _showMessage; }
            set { _showMessage = value; }
        }

        static Action<Context, LocalNotifications.Models.LocalNotification> _showNotification;
        public static Action<Context, LocalNotifications.Models.LocalNotification> ShowNotification
		{
			get { return _showNotification; }
			set { _showNotification = value; }
		}
    }
}
