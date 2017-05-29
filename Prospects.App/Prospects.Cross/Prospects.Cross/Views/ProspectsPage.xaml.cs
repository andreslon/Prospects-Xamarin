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
    public partial class ProspectsPage : ContentPage
    {
        public bool isLoaded { get; set; }
        public ProspectsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (this.BindingContext != null)
            {
                if (!isLoaded)
                {
                    MainViewModel main = (MainViewModel)this.BindingContext;
                    main.LoadProspects();
                    isLoaded = true;
                } 
            }
        }
    }
}