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

            // Setting Custom Show Message Handling
            LocalNotificationsAndroid.ShowMessage = (Context context, LocalNotifications.Models.LocalNotification localNotification) => 
            {
				var alertDialog = new AlertDialog.Builder(context)
									 .SetTitle("Custom Show Message Title")
									 .SetMessage("Custom Show Message Text")
									 .SetCancelable(false)
									 .SetPositiveButton("Ok", (sender, e) => { });
				alertDialog.Show();
            };

            // Setting Custom Show Notification Handling
            LocalNotificationsAndroid.ShowNotification = (Context context, LocalNotifications.Models.LocalNotification localNotification) => 
            {
				Notification.Builder builder = new Notification.Builder(ApplicationContext)
					.SetContentTitle("Custom Show Notification Title")
					.SetAutoCancel(true)
					.SetSmallIcon(Resource.Drawable.ic_media_play)
					.SetContentText("Custom Show Notification Text");

				Intent resultIntent = new Intent(this, typeof(MainActivity));				
				resultIntent.SetFlags(ActivityFlags.PreviousIsTop);

				PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.UpdateCurrent);

				builder.SetContentIntent(resultPendingIntent);

				var random = new System.Random(DateTime.Now.Millisecond);
				var id = random.Next();

                ((NotificationManager)context.GetSystemService(Android.Content.Context.NotificationService)).Notify(id, builder.Build());
            };
        }
    }
}
