﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prospects.Cross.Android.Services;

namespace Prospects.Cross.Droid
{
    [Activity(Label = "Prospects.Cross", Icon = "@drawable/icon", Theme = "@style/MainTheme",   ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            App.SetLocatorService(new LocatorService());
            LoadApplication(new App());
        }
    }
}

