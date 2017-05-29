using GalaSoft.MvvmLight;
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Enumerations;
using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Prospects.Cross.Infrastructure.Models.Responses;
using Prospects.Cross.Infrastructure.Models;

namespace Prospects.Cross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region interfaces
        public IApiService ApiService { get; set; }
        public IFileService FileService { get; set; }
        public INavigationService NavigationService { get; set; }
        public IDialogService DialogService { get; set; }
        #endregion
        #region properties
        private UserViewModel user;
        public UserViewModel User
        {
            get { return user; }
            set { Set(ref user, value); }
        }
        private ObservableCollection<ProspectViewModel> prospects;
        public ObservableCollection<ProspectViewModel> Prospects
        {
            get { return prospects; }
            set { Set(ref prospects, value); }
        }
        private ProspectViewModel selectedProspect;
        public ProspectViewModel SelectedProspect
        {
            get { return selectedProspect; }
            set { Set(ref selectedProspect, value); }
        }
        public ProspectViewModel ProspectEdited { get; set; }
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
        private bool _ErrorStatus;
        public bool ErrorStatus
        {
            get { return _ErrorStatus; }
            set { Set(ref _ErrorStatus, value); }
        }

        private string _StatusMessage;

        public string StatusMessage
        {
            get { return _StatusMessage; }
            set { Set(ref _StatusMessage, value); }
        }

        private ObservableCollection<Log> _Log;
        public ObservableCollection<Log> Log
        {
            get { return _Log; }
            set { Set(ref _Log, value); }
        }
        #endregion
        public MainViewModel(IApiService apiService, IFileService fileService, INavigationService navigationService, IDialogService dialogService)
        {
            ApiService = apiService;
            FileService = fileService;
            NavigationService = navigationService;
            DialogService = dialogService;
            User = DependencyContainer.LocatorService.Get<UserViewModel>();
            LoadMenuItems();
        }
        async private void SaveLog(string id)
        {
            List<Log> newLog = new List<Log>();
            if (await FileService.Exist("LogData"))
            {
                var LogsData = await FileService.LoadAsync<List<Log>>("LogData");
                if (LogsData != null)
                {
                    newLog = LogsData;
                }
            }
            newLog.Add(new Infrastructure.Models.Log { id = id, UpdatedAt = DateTime.Now });
            await FileService.SaveAsync("LogData", newLog);
        }
        async public void LoadProspects()
        {
            try
            {
                IsBusy = true;

                if (await FileService.Exist("ProspectsData"))
                {
                    var ProspectsData = await FileService.LoadAsync<List<ProspectResponse>>("ProspectsData");
                    if (ProspectsData != null)
                    {
                        FillProspects(ProspectsData);
                    }
                }
                else
                {
                    var apiResponse = await ApiService.GetProspects(User.Token);
                    if (apiResponse.HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        FillProspects(apiResponse.Response);
                        if (user.HoldSession)
                        {
                            await FileService.SaveAsync("ProspectsData", apiResponse.Response);
                        }
                    }
                    else
                    {
                        await DialogService.ShowMessage(LocalizedStrings.Get("strError"), "Error");
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
        async public void LoadLog()
        {
            try
            {
                IsBusy = true;
                Log = new ObservableCollection<Infrastructure.Models.Log>();
                if (await FileService.Exist("LogData"))
                {
                    var LogsData = await FileService.LoadAsync<List<Log>>("LogData");
                    foreach (var item in LogsData)
                    {
                        Log.Add(item);
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }


        }
        private void LoadMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            MenuItems.Add(new MenuItemViewModel()
            {
                Name = LocalizedStrings.Get("strProspectsList"),
                Type = PageTypes.Home,
                Icon = "ic_action_dashboard"
            });
            MenuItems.Add(new MenuItemViewModel()
            {
                Name = "Log",
                Type = PageTypes.Log,
                Icon = "ic_action_view_list"
            });
            MenuItems.Add(new MenuItemViewModel()
            {
                Name = LocalizedStrings.Get("strSignOut"),
                Type = PageTypes.SignOut,
                Icon = "ic_action_exit_to_app"
            });

        }
        public ICommand RetryCommand { get { return new RelayCommand(Retry); } }
        private void Retry()
        {
            Start();
        }
        async public void Start()
        {
            try
            {
                ErrorStatus = false;
                this.StatusMessage = LocalizedStrings.Get("strLoadingData");
                if (await FileService.Exist("UserData"))
                {
                    this.StatusMessage = LocalizedStrings.Get("strLoadingUSer");
                    var UserData = await FileService.LoadAsync<AuthResponse>("UserData");
                    if (UserData != null)
                    {
                        User.FillUserData(UserData);
                        NavigateTo(PageTypes.Home);
                    }
                    else
                    {
                        NavigateTo(PageTypes.Login);
                    }
                }
                else
                {
                    NavigateTo(PageTypes.Login);
                }
            }
            catch (Exception ex)
            {
                ErrorStatus = true;
                this.StatusMessage = ex.Message;
            }
        }
        async public void NavigateTo(PageTypes pageType)
        {
            switch (pageType)
            {
                case PageTypes.SignOut:
                    User.Clear();
                    await FileService.Delete("UserData");
                    await FileService.Delete("ProspectsData");
                    await FileService.Delete("LogData");
                    await NavigationService.Navigate(PageTypes.Login);
                    break;
                case PageTypes.Home:
                    await NavigationService.Navigate(pageType);
                    break;
                default:
                    await NavigationService.Navigate(pageType);
                    break;
            }

        }
        async public void UpdateProspect(ProspectViewModel NewProspect)
        {
            this.SelectedProspect = NewProspect;
            var newProspects = new ObservableCollection<ProspectViewModel>();
            foreach (var item in Prospects)
            {
                if (item.Id == NewProspect.Id)
                {
                    newProspects.Add(NewProspect);
                }
                else
                {
                    newProspects.Add(item);
                }
            }
            Prospects = newProspects;


            if (User.HoldSession)
            {
                var prospectsResponse = new List<ProspectResponse>();
                foreach (var item in Prospects)
                {
                    prospectsResponse.Add(ConvertToResponse(item));
                }
                await FileService.SaveAsync("ProspectsData", prospectsResponse);
            }

            SaveLog(NewProspect.Id);
        }
        internal ProspectViewModel CloneProspect(ProspectViewModel prospect)
        {
            return new ProspectViewModel
            {
                AcceptSearch = prospect.AcceptSearch,
                Address = prospect.Address,
                AppointableId = prospect.AppointableId,
                Callcenter = prospect.Callcenter,
                CampaignCode = prospect.CampaignCode,
                CityCode = prospect.CityCode,
                CreatedAt = prospect.CreatedAt,
                Disable = prospect.Disable,
                Id = prospect.Id,
                Name = prospect.Name,
                NeighborhoodCode = prospect.NeighborhoodCode,
                Observation = prospect.Observation,
                RejectedObservation = prospect.RejectedObservation,
                RoleId = prospect.RoleId,
                SchProspectIdentification = prospect.SchProspectIdentification,
                SectionCode = prospect.SectionCode,
                StatusCd = prospect.StatusCd,
                Surname = prospect.Surname,
                Telephone = prospect.Telephone,
                UpdatedAt = prospect.UpdatedAt,
                UserId = prospect.UserId,
                Visited = prospect.Visited,
                ZoneCode = prospect.ZoneCode,
            };
        }
        private void FillProspects(List<ProspectResponse> prospectsResponse)
        {
            Prospects = new ObservableCollection<ProspectViewModel>();
            foreach (var prospect in prospectsResponse)
            {
                Prospects.Add(new ProspectViewModel
                {
                    AcceptSearch = prospect.acceptSearch,
                    Address = prospect.address,
                    AppointableId = prospect.appointableId,
                    Callcenter = prospect.callcenter,
                    CampaignCode = prospect.campaignCode,
                    CityCode = prospect.cityCode,
                    CreatedAt = prospect.createdAt,
                    Disable = prospect.disable,
                    Id = prospect.id,
                    Name = prospect.name,
                    NeighborhoodCode = prospect.neighborhoodCode,
                    Observation = prospect.observation,
                    RejectedObservation = prospect.rejectedObservation,
                    RoleId = prospect.roleId,
                    SchProspectIdentification = prospect.schProspectIdentification,
                    SectionCode = prospect.sectionCode,
                    StatusCd = prospect.statusCd,
                    Surname = prospect.surname,
                    Telephone = prospect.telephone,
                    UpdatedAt = prospect.updatedAt,
                    UserId = prospect.userId,
                    Visited = prospect.visited,
                    ZoneCode = prospect.zoneCode,
                });
            }
        }
        internal ProspectResponse ConvertToResponse(ProspectViewModel prospect)
        {
            return new ProspectResponse
            {
                acceptSearch = prospect.AcceptSearch,
                address = prospect.Address,
                appointableId = prospect.AppointableId,
                callcenter = prospect.Callcenter,
                campaignCode = prospect.CampaignCode,
                cityCode = prospect.CityCode,
                createdAt = prospect.CreatedAt,
                disable = prospect.Disable,
                id = prospect.Id,
                name = prospect.Name,
                neighborhoodCode = prospect.NeighborhoodCode,
                observation = prospect.Observation,
                rejectedObservation = prospect.RejectedObservation,
                roleId = prospect.RoleId,
                schProspectIdentification = prospect.SchProspectIdentification,
                sectionCode = prospect.SectionCode,
                statusCd = prospect.StatusCd,
                surname = prospect.Surname,
                telephone = prospect.Telephone,
                updatedAt = prospect.UpdatedAt,
                userId = prospect.UserId,
                visited = prospect.Visited,
                zoneCode = prospect.ZoneCode,
            };
        }
    }
}
