﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prospects.Cross.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prospects.Cross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }
		protected override void OnAppearing()
		{
			if (this.BindingContext != null)
			{
				MainViewModel main = (MainViewModel)this.BindingContext;
				main.Start();
			}
		}
	}
}