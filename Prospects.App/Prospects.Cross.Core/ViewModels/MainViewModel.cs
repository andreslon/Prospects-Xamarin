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
        public MainViewModel(IApiService apiService)
        {
            ApiService = apiService;
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
    }
}
