using System;
using RendrKit.LocalNotifications.Models;

namespace RendrKit.LocalNotifications.Interfaces
{
    public interface ILocalNotificationService
    {
        LocalNotification AddNotification(LocalNotification notification); 
        void RemoveNotification(int notificationId);
    }
}
