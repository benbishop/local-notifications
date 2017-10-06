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
    }
}
