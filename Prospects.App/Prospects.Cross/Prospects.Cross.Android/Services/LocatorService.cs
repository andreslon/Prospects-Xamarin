using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Prospects.Cross.Android.Services;
using Prospects.Cross.Core.Services;
using Prospects.Cross.Core.ViewModels; 
using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Infrastructure.Services;
using Prospects.Cross.Services;
using System;

namespace Prospects.Cross.Android.Services
{
    public class LocatorService : ILocatorService
    {
        static LocatorService()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();
            //Cross Services
            SimpleIoc.Default.Register<IApiService, ApiService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IFileService, FileService>();
            SimpleIoc.Default.Register<IHttpClientService, HttpClientService>();
            SimpleIoc.Default.Register<IJsonService, JsonService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<INetworkService, NetworkService>();
            SimpleIoc.Default.Register<IToastService, ToastService>();
        }
        public T Get<T>() where T : class
        {
            return SimpleIoc.Default.GetInstance<T>();
        }
    }
}
