﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prospects.Cross.Controls
{
    public class ExtendedListView : ListView
    {
        private DataTemplateSelector currentItemSelector;

        public ExtendedListView()
        {
            this.ItemSelected += ExtendedListView_ItemSelected;
        }

        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(ExtendedListView), null, propertyChanged: OnDataTemplateSelectorChanged);

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        public static readonly BindableProperty IsSelectionEnabledProperty =
            BindableProperty.Create<ExtendedListView, bool>(o => o.IsSelectionEnabled, default(bool));

        public bool IsSelectionEnabled
        {
            get { return (bool)GetValue(IsSelectionEnabledProperty); }
            set { SetValue(IsSelectionEnabledProperty, value); }
        }

        void ExtendedListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ExtendedListView)sender;

            if (!listView.IsSelectionEnabled)
            {
                if (e == null) return;
                listView.SelectedItem = null;
            }
        }

        private static void OnDataTemplateSelectorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ExtendedListView)bindable).OnDataTemplateSelectorChanged((DataTemplateSelector)oldvalue, (DataTemplateSelector)newvalue);
        }

        protected virtual void OnDataTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            // check to see we don't have an ItemTemplate set
            if (ItemTemplate != null && newValue != null)
                throw new ArgumentException("Cannot set both ItemTemplate and ItemTemplateSelector", "ItemTemplateSelector");

            currentItemSelector = newValue;
        }

        protected override Cell CreateDefault(object item)
        {
            if (currentItemSelector != null)
            {
                var template = currentItemSelector.SelectTemplate(item, this);

                if (template != null)
                {
                    var templateInstance = template.CreateContent();
                    // see if it's a view or a cell
                    var templateView = templateInstance as View;
                    var templateCell = templateInstance as Cell;

                    if (templateView == null && templateCell == null)
                        throw new InvalidOperationException("DataTemplate must be either a Cell or a View");

                    if (templateView != null) // we got a view, wrap in a cell
                        templateCell = new ViewCell { View = templateView };

                    return templateCell;
                }
            }

            return base.CreateDefault(item);
        }
    }
}
