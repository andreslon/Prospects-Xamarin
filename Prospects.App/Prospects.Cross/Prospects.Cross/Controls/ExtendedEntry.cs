using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

[assembly:
    InternalsVisibleTo("Prospects.Cross.Android"),
    InternalsVisibleTo("Prospects.Cross.iOS"),
    InternalsVisibleTo("Prospects.Cross.UWP")]

namespace Prospects.Cross.Controls
{
    /// <summary>
    /// An extended entry control that allows the Font and text X alignment to be set
    /// </summary>
    public class ExtendedEntry : Entry
    {
         

        public void UpdateVisualElements()
        {

        }
        public static readonly BindableProperty IsNumericProperty =
        BindableProperty.Create<ExtendedEntry, bool>(
            p => p.IsNumeric, default(bool), BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                ((ExtendedEntry)bindable).UpdateVisualElements();
            });

        public bool IsNumeric
        {
            get { return (bool)GetValue(IsNumericProperty); }
            set { SetValue(IsNumericProperty, value); }
        }


        public static readonly BindableProperty IconProperty =
       BindableProperty.Create<ExtendedEntry, string>(
           p => p.Icon, default(string), BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
               ((ExtendedEntry)bindable).UpdateVisualElements();
           });

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

    }
}
