using System;
using Foundation;

namespace RendrKit.LocalNotifications.iOS.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime ToDateTime(this NSDate date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
									 new DateTime(2001, 1, 1, 0, 0, 0));
			return reference.AddSeconds(date.SecondsSinceReferenceDate);
		}

		public static NSDate ToNSDate(this DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
			{
				date = DateTime.SpecifyKind(date, DateTimeKind.Local);
			}
			return (NSDate)date;
		}
	}
}
