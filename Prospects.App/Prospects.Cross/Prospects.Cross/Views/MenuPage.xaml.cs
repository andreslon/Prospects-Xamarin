using Prospects.Cross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prospects.Cross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            lstMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                App.MasterDetailPage.IsPresented = false;
                ((MenuItemViewModel)e.SelectedItem).GoToMenuOptionCommand.Execute(null);
                lstMenu.SelectedItem = null;
            };
            if (Device.Idiom == TargetIdiom.Phone)
            {
                borderMenu.IsVisible = false;
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                lblHeaderEmail.Text = ((MainViewModel)this.BindingContext).User?.Email;
            }
            catch (Exception)
            {
            }

        }
    }
}