using Prospects.Cross.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Android.App;
using Xamarin.Forms;

namespace Prospects.Cross.Android.Services
{
	public class ToastService : IToastService
	{
		public void ShowToastLong(string message)
		{
			Toast toast = Toast.MakeText(Forms.Context, message, ToastLength.Long);
			toast.Show();
		}
		public void ShowToastShort(string message)
		{
			Toast toast = Toast.MakeText(Forms.Context, message, ToastLength.Short);
			toast.Show();
		}
	}
}
