using GalaSoft.MvvmLight.Command;
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Infrastructure.Resources;
using Prospects.Cross.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prospects.Cross.Core.ViewModels
{
    public class UserViewModel : ValidationBase
    {

        public INetworkService NetworkService { get; set; }
        public IDialogService DialogService { get; set; }
        public IFileService FileService { get; set; }
        public IApiService ApiService { get; set; }
        public IToastService ToastService { get; set; }
        public INavigationService NavigationService { get; set; }
        public UserViewModel(INetworkService networkService, IDialogService dialogService, IFileService fileService, IApiService apiService, IToastService toastService, INavigationService navigationService)
        {
            NetworkService = networkService;
            DialogService = dialogService;
            FileService = fileService;
            ApiService = apiService;
            ToastService = toastService;
            NavigationService = navigationService;
        }
        #region attributes 
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
        private bool holdSession;

        public bool HoldSession
        {
            get { return holdSession; }
            set { Set(ref holdSession, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { Set(ref password, value); }
        }
        public string Token { get; set; }
        #endregion

        #region command
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }
        async private void Login()
        {

            try
            {
                IsBusy = true;
                var main = DependencyContainer.LocatorService.Get<MainViewModel>();
                Validate();
                if (IsValid)
                {
                    if (NetworkService.IsNetworkAvailable)
                    {
                        var apiResponse = await ApiService.Login(this.Email, this.Password);
                        if (apiResponse.HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (apiResponse.Response.success)
                            {
                                this.Token = apiResponse.Response.authToken;
                                await NavigationService.Navigate(Infrastructure.Enumerations.PageTypes.Home);
                            }
                        }
                        else
                        {
                            await DialogService.ShowMessage(LocalizedStrings.Get("strUserIncorrect"), "Error");
                        }
                    }
                    else
                    {
                        ToastService.ShowToastLong(LocalizedStrings.Get("strWithOutConnectedInfo"));
                    }
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowMessage(ex.Message, "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion



        protected override void ValidateSelf()
        {
            var IsValid = true;
            var main = DependencyContainer.LocatorService.Get<MainViewModel>();
            if (string.IsNullOrEmpty(this.Email))
            {
                ValidationErrors["Email"] = LocalizedStrings.Get("strEmailRequired");
                IsValid = false;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                ValidationErrors["Password"] = LocalizedStrings.Get("strPassRequired");
                IsValid = false;
            }

            IsValid = IsValidEmail(this.Email);
            if (!IsValid)
            {
                ValidationErrors["Email"] = LocalizedStrings.Get("strEmailFormatIncorrect");
            }

            if (!IsValid)
            {
                DialogService.ShowMessage(LocalizedStrings.Get("strFieldsRequired"), "Error");
            }
        }

        public bool IsValidEmail(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
