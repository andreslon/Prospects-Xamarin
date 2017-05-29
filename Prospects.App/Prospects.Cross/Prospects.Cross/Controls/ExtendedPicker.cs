using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prospects.Cross.Controls
{
    public class ExtendedPicker : Picker
    {
        public ExtendedPicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;

            if (SelectedItem != null)
            {
                SelectedIndex = Items.IndexOf(SelectedItem.ToString());
            }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<ExtendedPicker, Color>(o => o.TextColor, default(Color));

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ExtendedPicker, IEnumerable<object>>(o => o.ItemsSource, default(IEnumerable<object>), propertyChanged: OnItemsSourcePropertyChanged);

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<ExtendedPicker, object>(o => o.SelectedItem, default(object), propertyChanged: OnSelectedItemChanged);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable<object> oldvalue, IEnumerable<object> newvalue)
        {
            var picker = bindable as ExtendedPicker;

            if (picker != null)
            {
                picker.Items.Clear();

                if (newvalue != null)
                {
                    foreach (var item in newvalue)
                    {
                        picker.Items.Add(item.ToString());
                    }
                }


                if (picker.Items.Count > 0)
                {
                    picker.SelectedIndex = 0;
                }
            }

        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex >= 0 && Items.Count > 0 && SelectedIndex <= Items.Count - 1)
            {
                if (SelectedIndex > Items.Count - 1)
                {
                    SelectedItem = this.ItemsSource.ElementAt(Items.Count - 1);
                }
                else
                {
                    SelectedItem = this.ItemsSource.ElementAt(SelectedIndex);
                }
            }

        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as ExtendedPicker;

            if (newvalue != null)
            {
                var index = picker.Items.IndexOf(newvalue.ToString());

                if (picker.SelectedIndex != index)
                {
                    picker.SelectedIndex = index;
                }
            }

        }
    }
}