using System;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Java.Util;
using RendrKit.LocalNotifications.Interfaces;
using Xamarin.Forms;
using RendrKit.LocalNotifications.Models;

namespace RendrKit.LocalNotification.Samples.Droid
{
	//public class LocalNotificationService : ILocalNotificationService
	//{
	//	private Context _context;
	//	public Context Context
	//	{
	//		get
	//		{
	//			return _context ?? Forms.Context;
	//		}
	//		set { _context = value; }
	//	}

	//	private AlarmManager _alarmManager;
	//	public AlarmManager AlarmManager
	//	{
	//		get
	//		{
	//			if (_alarmManager != null)
	//			{
	//				return _alarmManager;
	//			}

	//			var activity = Context;

	//			return _alarmManager ?? (_alarmManager = (AlarmManager)activity.GetSystemService(Android.Content.Context.AlarmService));
	//		}
	//	}

	//	public LocalNotifications.Models.LocalNotification AddNotification(LocalNotifications.Models.LocalNotification notification)
	//	{
	//		var intent = CreateAlarmIntent(notification);
	//		var pendingIntent = CreatePendingIntent(intent, PendingIntentFlags.CancelCurrent);

	//		var calendar = Calendar.Instance;
	//		calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
	//		calendar.Set(
	//			notification.FireDate.Year,
	//			notification.FireDate.Month - 1,
	//			notification.FireDate.Day,
	//			notification.FireDate.Hour,
	//			notification.FireDate.Minute);

	//		AlarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);

	//		Debug.WriteLine($"RendrKit.LocalNotifications: ADDED LOCAL NOTIFICATION: FireDate: {notification.FireDate} Message: {notification.Text}");

	//		return notification;
	//	}

	//	private Intent CreateAlarmIntent(LocalNotifications.Models.LocalNotification notification)
	//	{
	//		Intent intent = new Intent(Context, typeof(ReminderReceiver));

	//		var contents = string.Empty;
	//		intent.PutExtra("item_json", contents);
	//		return intent;
	//	}

	//	private PendingIntent CreatePendingIntent(Intent intent, PendingIntentFlags flag)
	//	{
	//		var random = new System.Random(DateTime.Now.Millisecond);
	//		var id = random.Next();

	//		return PendingIntent.GetBroadcast(Context, id, intent, flag);
	//	}
	//}

	//[BroadcastReceiver(Exported = true)]
	//public class ReminderReceiver : Android.Support.V4.Content.WakefulBroadcastReceiver
	//{
	//	public override void OnReceive(Context context, Intent intent)
	//	{
	//		ShowMessage();
	//	}

	//	private void ShowMessage()
	//	{
	//		var alertDialog = new AlertDialog.Builder(Xamarin.Forms.Forms.Context)
	//							 .SetTitle("RendrKit.LocalNotifications")
	//							 .SetMessage("Testing")
	//							 .SetCancelable(false)
	//							 .SetPositiveButton("Ok", (sender, e) => { });
	//		alertDialog.Show();
	//	}
	//}
}
