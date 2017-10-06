using System;

namespace RendrKit.LocalNotifications.Models
{
    public class LocalNotification
    {
        public string Id
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public DateTime FireDate
        {
            get;
            set;
        }
    }
}
