using MonkeyCache.FileStore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCalendarProject.Services;
using XamarinCalendarProject.Views;

namespace XamarinCalendarProject
{
    public partial class App : Application
    {
        private static ServiceDependency _Locator;
        public static ServiceDependency Locator
        {
            get
            {
                return _Locator = _Locator ?? new ServiceDependency();
            }
        }
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "calendarioxamarin";
            MainPage = new MainCalendarioView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
