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
    public partial class LogPage : ContentPage
    {
        public LogPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (this.BindingContext != null)
            {
                MainViewModel main = (MainViewModel)this.BindingContext;
                main.LoadLog();
            }
        }
    }
}