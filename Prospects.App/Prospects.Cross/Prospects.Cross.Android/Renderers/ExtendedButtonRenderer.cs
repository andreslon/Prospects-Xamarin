using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Prospects.Cross.Controls;
using Prospects.Cross.Android.Renderers;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace Prospects.Cross.Android.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Element != null && Control != null)
            {
                var element = (ExtendedButton)Element;

                var scale = Forms.Context.Resources.DisplayMetrics.Density;

                var left = (int)(Convert.ToInt32(element.Padding.Left) * scale + 0.5f);
                var top = (int)(Convert.ToInt32(element.Padding.Top) * scale + 0.5f);
                var right = (int)(Convert.ToInt32(element.Padding.Right) * scale + 0.5f);
                var bottom = (int)(Convert.ToInt32(element.Padding.Bottom) * scale + 0.5f); 
                Control.SetPadding(left, top, right, bottom);
 
            }
        }
    }
}