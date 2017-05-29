using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prospects.Cross.Controls
{
    public class ExtendedCheck : View
    {

        public void UpdateVisualElements()
        {
            
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }
        public static readonly BindableProperty CheckedProperty =
        BindableProperty.Create<ExtendedCheck, bool>(
            p => p.Checked, default(bool), BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                ((ExtendedCheck)bindable).UpdateVisualElements();
            });
         
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create<ExtendedCheck, string>(
            p => p.Text, string.Empty, BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                ((ExtendedCheck)bindable).UpdateVisualElements();
            });



        public bool Lock
        {
            get { return (bool)GetValue(LockProperty); }
            set { SetValue(LockProperty, value); }
        }
        public static readonly BindableProperty LockProperty =
        BindableProperty.Create<ExtendedCheck, bool>(
            p => p.Lock, default(bool), BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                ((ExtendedCheck)bindable).UpdateVisualElements();
            });


        public bool Sent
        {
            get { return (bool)GetValue(SentProperty); }
            set { SetValue(SentProperty, value); }
        }
        public static readonly BindableProperty SentProperty =
        BindableProperty.Create<ExtendedCheck, bool>(
            p => p.Sent, default(bool), BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                ((ExtendedCheck)bindable).UpdateVisualElements();
            });

    }
}
