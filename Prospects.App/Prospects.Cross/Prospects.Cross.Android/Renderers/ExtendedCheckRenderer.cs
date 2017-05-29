
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Prospects.Cross.Controls;
using Prospects.Cross.Android.Renderers;
using DroidWid = Android.Widget;
using DroidGra = Android.Graphics;
[assembly: ExportRenderer(typeof(ExtendedCheck), typeof(ExtendedCheckRenderer))]

namespace Prospects.Cross.Android.Renderers
{
    public class ExtendedCheckRenderer : ViewRenderer<ExtendedCheck, CheckBox>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCheck> e)
        {
            base.OnElementChanged(e);
            CheckBox control = new DroidWid.CheckBox(this.Context);
            control.Checked = e.NewElement.Checked;
            control.CheckedChange += Control_CheckedChange;
            control.Text = e.NewElement.Text;
            control.SetTextColor(DroidGra.Color.Rgb(60, 60, 60));
            control.Enabled = !e.NewElement.Lock;

            this.SetNativeControl(control);
            if (e.NewElement.Checked)
            {
                if (e.NewElement.Sent)
                {
                    Control.Enabled = false;
                    //Control.SetButtonDrawable(Droid.Resource.Drawable.ic_check_box_green);
                }
                else {
                    //Control.SetButtonDrawable(Droid.Resource.Drawable.ic_check_box_blue);
                }
                
            }
            else if (e.NewElement.Lock)
            {
                Control.Enabled = false;
                //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline_lock);
            }
            else
            {
                //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline);
            }


        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals("Lock"))
            {
                Control.Enabled = !((ExtendedCheck)sender).Lock;
                if (Control.Enabled)
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline);
                }
                else
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline_lock);
                }
            }
            else if (e.PropertyName.Equals("Sent"))
            {
                Control.Enabled = false;
                if (((ExtendedCheck)sender).Sent)
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_green);
                }
            }
            else if (e.PropertyName.Equals("Checked"))
            {
                if (((ExtendedCheck)sender).Checked)
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_blue);
                }
                else
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline);
                }
            }

        }

        private void Control_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            try
            {
                
                Element.Checked = e.IsChecked;
                if (e.IsChecked)
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_blue);
                }
                else
                {
                    //Control.SetButtonDrawable(Resource.Drawable.ic_check_box_outline);
                }


            }
            catch (Exception)
            {

            }
        }
    }
}