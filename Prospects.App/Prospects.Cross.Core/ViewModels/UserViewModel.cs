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
using Prospects.Cross.Infrastructure.Models.Responses;

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
#if DEBUG
                this.Email = "directo@directo.com";
                this.Password = "directo123";
#endif

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
                                IsBusy = false;
                                if (HoldSession)
                                {
                                    apiResponse.Response.holdSession = true;
                                    await FileService.SaveAsync("UserData", apiResponse.Response);
                                }
                                FillUserData(apiResponse.Response);
                                await NavigationService.Navigate(Infrastructure.Enumerations.PageTypes.Home);
                            }
                        }
                        else
                        {
                            IsBusy = false;
                            await DialogService.ShowMessage(LocalizedStrings.Get("strUserIncorrect"), "Error");
                        }
                    }
                    else
                    {
                        IsBusy = false;
                        ToastService.ShowToastLong(LocalizedStrings.Get("strWithOutConnectedInfo"));
                    }
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await DialogService.ShowMessage(ex.Message, "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void FillUserData(AuthResponse response)
        {
            this.Token = response.authToken;
            this.Email = response.email;
            this.HoldSession = response.holdSession;
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
            else {
                IsValid = IsValidEmail(this.Email);
                if (!IsValid)
                {
                    ValidationErrors["Email"] = LocalizedStrings.Get("strEmailFormatIncorrect");
                }
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                ValidationErrors["Password"] = LocalizedStrings.Get("strPassRequired");
                IsValid = false;
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

        internal void Clear()
        {
            this.Token = string.Empty; 
            this.Email = string.Empty;
            this.HoldSession = false;
            this.Password = string.Empty; 
        }
    }
}
