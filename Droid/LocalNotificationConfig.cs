using System;
using Android.Content;

namespace RendrKit.LocalNotifications.Droid
{
    public class LocalNotificationConfig
    {
        private Context _context;

        public LocalNotificationConfig(Context context)
        {
            _context = context;
        }

        public string Title
        {
            get 
            {
                return LocalNotificationsAndroid.Title ?? _context.ApplicationInfo.LoadLabel(_context.PackageManager);
            }
        }

        public int IconResource
        {
            get 
            {
                if (LocalNotificationsAndroid.IconResource.HasValue)
                {
                    return LocalNotificationsAndroid.IconResource.Value;
                }

                return _context.Resources.GetIdentifier("icon", "drawable", _context.PackageName);                    
            }
        }
    }
}
