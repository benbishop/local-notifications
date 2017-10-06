using System;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Java.Util;
using RendrKit.LocalNotifications.Interfaces;
using RendrKit.LocalNotifications.Models;
using Xamarin.Forms;

namespace RendrKit.LocalNotifications.Droid.Implementations
{
	public class LocalNotificationService : ILocalNotificationService
	{
		private Context _context;
		public Context Context
		{
			get
			{
				return _context ?? Forms.Context;
			}
			set { _context = value; }
		}

		private AlarmManager _alarmManager;
		public AlarmManager AlarmManager
		{
			get
			{
				if (_alarmManager != null)
				{
					return _alarmManager;
				}

				var activity = Context;

				return _alarmManager ?? (_alarmManager = (AlarmManager)activity.GetSystemService(Android.Content.Context.AlarmService));
			}
		}

		public LocalNotifications.Models.LocalNotification AddNotification(LocalNotifications.Models.LocalNotification notification)
		{
			var intent = CreateAlarmIntent(notification);
			var pendingIntent = CreatePendingIntent(intent, PendingIntentFlags.CancelCurrent);

			var calendar = Calendar.Instance;
			calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
			calendar.Set(
				notification.FireDate.Year,
				notification.FireDate.Month - 1,
				notification.FireDate.Day,
				notification.FireDate.Hour,
				notification.FireDate.Minute);

			AlarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);

			Debug.WriteLine($"RendrKit.LocalNotifications: ADDED LOCAL NOTIFICATION: FireDate: {notification.FireDate} Message: {notification.Text}");

			return notification;
		}

		private Intent CreateAlarmIntent(LocalNotifications.Models.LocalNotification notification)
		{
			Intent intent = new Intent(Context, typeof(ReminderReceiver));

			var contents = string.Empty;
			intent.PutExtra("item_json", contents);
			return intent;
		}

		private PendingIntent CreatePendingIntent(Intent intent, PendingIntentFlags flag)
		{
			var random = new System.Random(DateTime.Now.Millisecond);
			var id = random.Next();

			return PendingIntent.GetBroadcast(Context, id, intent, flag);
		}
	}

	[BroadcastReceiver(Exported = true)]
	public class ReminderReceiver : Android.Support.V4.Content.WakefulBroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
            if (IsForeGround())
            {
                ShowMessage();
            }
			else
			{
				var newIntent = new Intent(context, typeof(AlarmServiceIntentService));
				newIntent.ReplaceExtras(intent);
				StartWakefulService(context, newIntent);
			}
		}

		private void ShowMessage()
		{            
            var config = new LocalNotificationConfig(Xamarin.Forms.Forms.Context);

            var alertDialog = new AlertDialog.Builder(Xamarin.Forms.Forms.Context)
                                 .SetTitle(config.Title)
                                 .SetMessage("Testing")
                                 .SetCancelable(false)
                                 .SetPositiveButton("Ok", (sender, e) => { });
            alertDialog.Show();
		}

        private bool IsForeGround()
        {
            ActivityManager.RunningAppProcessInfo appProcessInfo = new ActivityManager.RunningAppProcessInfo();
            ActivityManager.GetMyMemoryState(appProcessInfo);
            return (appProcessInfo.Importance == Importance.Foreground || appProcessInfo.Importance == Importance.Visible);
        }
	}

	[Service]
	public class AlarmServiceIntentService : IntentService
	{
		private NotificationManager _notificationManager;
		public NotificationManager NotificationManager
		{
			get
			{
				if (_notificationManager != null)
				{
					return _notificationManager;
				}

				return _notificationManager ?? (_notificationManager = (NotificationManager)ApplicationContext.GetSystemService(Android.Content.Context.NotificationService));
			}
		}

		public AlarmServiceIntentService() : base("AlarmServiceIntentService")
		{

		}

		protected override void OnHandleIntent(Intent intent)
		{			
            var message = string.Empty;
			SendNotification();
			
			Android.Support.V4.Content.WakefulBroadcastReceiver.CompleteWakefulIntent(intent);		
		}

		private void SendNotification()
		{
            var config = new LocalNotificationConfig(ApplicationContext);

			Notification.Builder builder = new Notification.Builder(ApplicationContext)
				.SetContentTitle(config.Title)
				.SetAutoCancel(true)
                .SetSmallIcon(config.IconResource)
				.SetContentText("Testing");

            //Intent resultIntent = new Intent(this, typeof(SplashScreenActivity));
            var resultIntent = new Intent();
			resultIntent.SetFlags(ActivityFlags.PreviousIsTop);

			PendingIntent resultPendingIntent = PendingIntent.GetActivity(this.ApplicationContext, 0, resultIntent, PendingIntentFlags.UpdateCurrent);

			builder.SetContentIntent(resultPendingIntent);

			var random = new System.Random(DateTime.Now.Millisecond);
			var id = random.Next();

            NotificationManager.Notify(id, builder.Build());
		}
	}
}
