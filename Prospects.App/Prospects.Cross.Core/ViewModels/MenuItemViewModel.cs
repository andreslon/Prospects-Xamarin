﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Prospects.Cross.Infrastructure;
using Prospects.Cross.Infrastructure.Enumerations;
using System.Windows.Input;

namespace Prospects.Cross.Core.ViewModels
{
    public class MenuItemViewModel : ViewModelBase
    {
        public MenuItemViewModel()
        {
        } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public PageTypes Type { get; set; }
        public ICommand GoToMenuOptionCommand { get { return new RelayCommand(GoToMenuOption); } }
        private void GoToMenuOption()
        {
            switch (Type)
            {
                case PageTypes.Splash:
                    break;
                case PageTypes.Login:
                    break;
                case PageTypes.SignOut:
                    break;
                case PageTypes.Home:
                    break;
                case PageTypes.EditProspect:
                    break; 
            }
        }
    }
}