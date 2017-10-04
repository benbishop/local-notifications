using RendrKit.LocalNotifications.Interfaces;
using Xamarin.Forms;

namespace RendrKit.LocalNotification.Samples
{
    public partial class App : Application
    {
        public static ILocalNotificationService NotificationService { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
