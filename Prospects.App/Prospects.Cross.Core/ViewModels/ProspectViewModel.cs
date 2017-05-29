using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prospects.Cross.Core.ViewModels
{
    public class ProspectViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; set; }
        public ProspectViewModel()
        {
            NavigationService = DependencyContainer.LocatorService.Get<INavigationService>();
        }
        #region commands
        public ICommand SelectCommand { get { return new RelayCommand(Select); } }

        private void Select()
        {
            var main = DependencyContainer.LocatorService.Get<MainViewModel>();
            main.SelectedProspect = this;
            NavigationService.Navigate(Infrastructure.Enumerations.PageTypes.ProspectDetail);

        }

        public ICommand EditCommand { get { return new RelayCommand(Edit); } }

        private void Edit()
        {
            NavigationService.Navigate(Infrastructure.Enumerations.PageTypes.EditProspect);
        }

        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private void Save()
        {

        }
        #endregion

        #region attributes


        public string FullName
        {
            get { return Surname + " " + Name; }
        }


        public string Id { get; set; }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { Set(ref _Name, value); }
        }

        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string SchProspectIdentification { get; set; }
        public string Address { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; } 
        private int _StatusCd; 
        public int StatusCd
        {
            get { return _StatusCd; }
            set { Set(ref _StatusCd, value); }
        }

        public string ZoneCode { get; set; }
        public string NeighborhoodCode { get; set; }
        public string CityCode { get; set; }
        public string SectionCode { get; set; }
        public int RoleId { get; set; }
        public object AppointableId { get; set; }
        public object RejectedObservation { get; set; }
        public string Observation { get; set; }
        public bool Disable { get; set; }
        public bool Visited { get; set; }
        public bool Callcenter { get; set; }
        public bool AcceptSearch { get; set; }
        public string CampaignCode { get; set; }
        public object UserId { get; set; }

        #endregion

    }
}
