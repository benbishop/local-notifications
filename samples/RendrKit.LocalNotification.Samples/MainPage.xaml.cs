using Xamarin.Forms;
using System;
using RendrKit.LocalNotifications.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace RendrKit.LocalNotification.Samples
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<LocalNotifications.Models.LocalNotification> Notifications
        {
            get;
            set;
        }

        public MainPage()
        {
            InitializeComponent();

            Notifications = new ObservableCollection<LocalNotifications.Models.LocalNotification>();

            this.NotificationTimePicker.Time = (DateTime.Now - DateTime.Now.Date);
            this.AddNotificationButton.Clicked += AddNotificationButton_Clicked;
            NotificationsList.SetBinding(ListView.ItemsSourceProperty, "Notifications");
            NotificationsList.BindingContext = this;
        }

        void AddNotificationButton_Clicked(object sender, System.EventArgs e)
        {
            var notification = new LocalNotifications.Models.LocalNotification()
            {
                Text = NotificationText.Text,
                FireDate = DateTime.Now.Date.AddHours(this.NotificationTimePicker.Time.Hours).AddMinutes(this.NotificationTimePicker.Time.Minutes)
            };

            App.NotificationService.AddNotification(notification);
            Notifications.Add(notification);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var notification = button.BindingContext as LocalNotifications.Models.LocalNotification;
            App.NotificationService.RemoveNotification(notification.Id);
            Notifications.Remove(notification);
        }
    }
}
