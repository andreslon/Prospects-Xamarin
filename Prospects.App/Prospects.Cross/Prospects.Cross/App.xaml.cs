using Prospects.Cross.Core.ViewModels;
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Prospects.Cross
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetailPage { get; set; }
        public static NavigationPage NavigationPage { get; set; }
        public static ILocatorService LocatorService { get; set; }

        public App()
        {
            InitializeComponent();
            SetSplashPage();
        }
        public static void SetProspectsPage()
        {
            MasterDetailPage = new MasterDetailPage() { MasterBehavior = MasterBehavior.SplitOnLandscape };
            NavigationPage = new NavigationPage(new ProspectsPage());
            MasterDetailPage.Master = new MenuPage();
            MasterDetailPage.Detail = NavigationPage;
            Current.MainPage = MasterDetailPage;
        }
        public static void SetSplashPage()
        {
            var page = new SplashPage();
            page.BindingContext = LocatorService.Get<MainViewModel>();
            Current.MainPage = page;
        }
        public static void SetLoginPage()
        {
            var page = new LoginPage();
            Current.MainPage = page;
        }
        public static void SetDetailPage(Page page)
        {
            NavigationPage = new NavigationPage(page);
            MasterDetailPage.Detail = NavigationPage;
        }

        async public static void SetPushPage(Page page)
        {
            NavigationPage.SetBackButtonTitle(page, "Atrás");
            await NavigationPage.PushAsync(page);
        }
        async public static void SetNavigationPage(Page page)
        {
            NavigationPage.SetBackButtonTitle(page, "Atrás");
            await NavigationPage.PushAsync(page);
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
