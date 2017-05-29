using System;
using System.ComponentModel;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android;
using Prospects.Cross.Controls;
using Prospects.Cross.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TextChangedEventArgs = Android.Text.TextChangedEventArgs;
using View = Android.Views.View; 
using Android.Graphics.Drawables;
using wid = Android.Widget;
[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace Prospects.Cross.Android.Renderers
{
    public class ExtendedEntryRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<ExtendedEntry, View>
    {
        private TextInputLayout _nativeView;

        private TextInputLayout NativeView
        {
            get { return _nativeView ?? (_nativeView = InitializeNativeView()); }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedEntry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var ctrl = CreateNativeControl();

                var editText = ctrl.FindViewById<wid.EditText>(Droid.Resource.Id.textInput);

                if (!string.IsNullOrEmpty(e.NewElement.Icon))
                {
                    editText.SetCompoundDrawablesWithIntrinsicBounds(Resources.GetIdentifier(e.NewElement.Icon, "drawable", Context.PackageName), 0, 0, 0);
                }
                else
                {
                    editText.SetCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
                }
                 
                SetNativeControl(ctrl);

                SetText();
                SetFontSize();
                SetHintText();
                SetBackgroundColor();
                SetTextColor();
                SetIsPassword();
                if (e.NewElement.IsNumeric)
                {
                    NativeView.EditText.InputType = InputTypes.ClassNumber;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                SetHintText();
            }
            if (e.PropertyName == Entry.FontSizeProperty.PropertyName)
            {
                SetFontSize();
            }

            if (e.PropertyName == Entry.TextColorProperty.PropertyName)
            {
                SetTextColor();
            }

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                SetBackgroundColor();
            }

            if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
            {
                SetIsPassword();
            }

            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                SetText();
            }
        }

        private void EditTextOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            try
            {
                Element.Text = textChangedEventArgs.Text.ToString();
                //NativeView.EditText.SetSelection(Element.Text.Length);
            }
            catch (Exception)
            {

            }
        }

        private void SetText()
        {
            //NativeView.EditText.Text = Element.Text;
        }
        private void SetFontSize()
        {
            NativeView.EditText.TextSize = (float)Element.FontSize;
        }
        private void SetIsPassword()
        {
            NativeView.EditText.InputType = Element.IsPassword
                ? InputTypes.TextVariationPassword | InputTypes.ClassText
                : NativeView.EditText.InputType;
        }



        public void SetBackgroundColor()
        {
            NativeView.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
        }

        private void SetHintText()
        {
            NativeView.Hint = Element.Placeholder;
        }

        private void SetTextColor()
        {
            if (Element.TextColor == Color.Default)
            {
                NativeView.EditText.SetTextColor(NativeView.EditText.TextColors);
            }
            else
            {
                NativeView.EditText.SetTextColor(Element.TextColor.ToAndroid());
            }
        }

        private TextInputLayout InitializeNativeView()
        {
            var view = FindViewById<TextInputLayout>(Droid.Resource.Id.textInputLayout);
            view.EditText.TextChanged += EditTextOnTextChanged;
            return view;
        }

        protected override View CreateNativeControl()
        {
            var control = LayoutInflater.From(Context).Inflate(Droid.Resource.Layout.TextInputLayout, null);

            return control;
        }
    }
}