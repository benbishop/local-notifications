using System;

namespace RendrKit.LocalNotifications.Models
{
    public class LocalNotification
    {
        public int Id
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
