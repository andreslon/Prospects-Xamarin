using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms;
using DroidResource = Android.Resource;
using Xamarin.Forms.Platform.Android;
using Prospects.Cross.Controls;
using Prospects.Cross.Android.Renderers;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRenderer))]
namespace Prospects.Cross.Android.Renderers
{
    public class ExtendedPickerRenderer : ViewRenderer<View, Spinner>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                var element = (ExtendedPicker)Element;

                var spinner = new Spinner(Context);

                spinner.ItemSelected += spinner_ItemSelected;

                if (element.BackgroundColor != Color.Default)
                    spinner.SetBackgroundColor(element.BackgroundColor.ToAndroid());

                SetNativeControl(spinner);

                LoadItems();

                SetItem();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ExtendedPicker.ItemsSourceProperty.PropertyName)
            {
                LoadItems();
            }
            else if (e.PropertyName == ExtendedPicker.SelectedItemProperty.PropertyName)
            {
                SetItem();
            }
        }

        private void SetItem()
        {
            var element = (ExtendedPicker)Element;

            if (element.SelectedItem != null)
            {
                var adapter = (ArrayAdapter<object>)Control.Adapter;
                if (adapter == null)
                {
                    return;
                }
                if (adapter.Count > 0)
                {
                    var position = adapter.GetPosition(element.SelectedItem);
                    Control.SetSelection(position);
                }
            }
        }

        private void LoadItems()
        {
            var element = (ExtendedPicker)Element;

            if (element.ItemsSource != null)
            {
                var items = element.ItemsSource.ToList();
                var adapter = new ArrayAdapter<object>(Forms.Context, DroidResource.Layout.SimpleSpinnerItem, items);

                adapter.SetDropDownViewResource(DroidResource.Layout.SimpleSpinnerDropDownItem);

                Control.Adapter = adapter;
            }
        }

        void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var element = (ExtendedPicker)Element;
            var spinner = (Spinner)sender;

            var spinnerText = (TextView)e.Parent.GetChildAt(0);

            if (spinnerText != null)
            {
                if (element.TextColor != Color.Default)
                    spinnerText.SetTextColor(element.TextColor.ToAndroid());
            }

            var adapter = (ArrayAdapter<object>)spinner.Adapter;
            var item = adapter.GetItem(e.Position);

            element.SelectedItem = item;
        }
    }
}