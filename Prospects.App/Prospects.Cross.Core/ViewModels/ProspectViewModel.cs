using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Prospects.Cross.Infrastructure;
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
        public ProspectViewModel()
        {

        }
        #region commands
        public ICommand SelectCommand { get { return new RelayCommand(Select); } }

         private void Select()
        {
            //var main = App.LocatorService.Get<MainViewModel>();
            //main.SelectedPlace = this;
            //App.SetNavigationPage(new PlaceDetailPage());
        }
        #endregion

        #region attributes
        #endregion
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string SchProspectIdentification { get; set; }
        public string Address { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int StatusCd { get; set; }
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

    }
}
