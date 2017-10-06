using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RendrKit.LocalNotifications.Droid.Implementations;
using RendrKit.LocalNotifications.Droid;

namespace RendrKit.LocalNotification.Samples.Droid
{
    [Activity(Label = "RendrKit.LocalNotification.Samples.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {            
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            App.NotificationService = new LocalNotificationService();

            // By Default We use for Title the Android:Name attribute and for icon a drawable with the name "icon", you can set them using the properties below
            LocalNotificationsAndroid.IconResource = Resource.Drawable.ic_media_play;
            LocalNotificationsAndroid.Title = "Testing";
        }
    }
}
