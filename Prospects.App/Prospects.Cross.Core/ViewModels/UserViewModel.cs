using GalaSoft.MvvmLight.Command; 
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Infrastructure.Resources;
using Prospects.Cross.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public UserViewModel()
        {

        }
        #region attributes
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
                //ProgressDialogService.Show();
                await LoginServer();
            }
            catch (Exception)
            {
                //Error
            }
            finally
            {
                //ProgressDialogService.Hide();
            }
        }
        #endregion

        async public Task LoginServer()
        {
            var main = DependencyContainer.LocatorService.Get<MainViewModel>();
            try
            {
                Validate();
                if (IsValid)
                {
                    if (NetworkService.IsNetworkAvailable)
                    {
                        //ProgressDialogService.Show();
                        var apiResponse = await ApiService.Login(this.Email, this.Password);
                        if (apiResponse.HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (apiResponse.Response.success)
                            {
                                this.Token = apiResponse.Response.authToken;
                            }
                        }
                        else
                        {

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
                await DialogService.ShowMessage(ex.Message, LocalizedStrings.Get("strError"));
            }
            finally
            {
                //ProgressDialogService.Hide();
            }
        }


        protected override void ValidateSelf()
        {
            var isValid = true;
            var main = DependencyContainer.LocatorService.Get<MainViewModel>();
            if (string.IsNullOrEmpty(this.email))
            {
                ValidationErrors["Email"] = LocalizedStrings.Get("strEmailRequired");
                isValid = false;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                ValidationErrors["Password"] = LocalizedStrings.Get("strPassRequired");
                isValid = false;
            }

            if (!isValid)
            {
                DialogService.ShowMessage(LocalizedStrings.Get("strFieldRequired"), "");
            }
        }
    }
}
