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

namespace Prospects.Cross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }

        private List<ProspectViewModel> prospects;


        public List<ProspectViewModel> Prospects
        {
            get { return prospects; }
            set { Set(ref prospects, value); }
        }

        public ProspectViewModel SelectedProspect { get; set; }
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public IApiService ApiService { get; set; }
        public IFileService FileService { get; set; }
        public INavigationService NavigationService { get; set; }

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

        public MainViewModel(IApiService apiService, IFileService fileService, INavigationService navigationService)
        {
            ApiService = apiService;
            FileService = fileService;
            NavigationService = navigationService;
            LoadMenuItems();
        }
        async public void LoadProspects()
        {
            var apiResponse = await ApiService.GetProspects(User.Token);
            if (apiResponse.HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Prospects = new List<ProspectViewModel>();
                foreach (var prospect in apiResponse.Response)
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
            else
            {
                //await App.NavigationPage.DisplayAlert("Ups!", "Ha ocurrido un error obteniendo los prospectos del servidor.", "Aceptar");
            }
        }

        private void LoadMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            MenuItems.Add(new MenuItemViewModel()
            {
                Name = LocalizedStrings.Get("strProspectsList"),
                Type = PageTypes.Home,
                Icon = "ic_home_white_24dp"
            });
            MenuItems.Add(new MenuItemViewModel()
            {
                Name = LocalizedStrings.Get("strSignOut"),
                Type = PageTypes.SignOut,
                Icon = "ic_star_rate_white_18dp"
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
                await Task.Delay(10000); 
                if (await FileService.Exist("UserData"))
                {
                    this.StatusMessage = LocalizedStrings.Get("strLoadingUSer");
                    //var MonitorData = await FileService.LoadAsync<MonitorResponse>("MonitorData");
                    //if (MonitorData != null)
                    //{

                    //}
                    //else
                    //{
                    //     NavigateTo(PageTypes.Login);
                    //}
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
                    //MonitorViewModel.ClearMonitor();
                    //MonitorViewModel.HoldSession = false;
                    //await fileService.Delete("AuthToken");
                    //await fileService.Delete("MonitorData");
                    //await fileService.Delete("RoutesData");
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

    }
}
