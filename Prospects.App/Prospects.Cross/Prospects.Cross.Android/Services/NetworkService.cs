using Prospects.Cross.Infrastructure.Interfaces;
using System;
using Android.Net;
using Xamarin.Forms;
using Android.Content;

namespace Prospects.Cross.Android.Services
{
	public class NetworkService : INetworkService
	{
		public event EventHandler NetworkAvailabilityChanged;

		public bool IsNetworkAvailable
		{
			//
			//#if DEBUG
			//            get { return true; }
			//#else
			get { return CheckNetwork(); }
			//#endif


		}


		private bool CheckNetwork()
		{
			try
			{
				var connectivityManager = (ConnectivityManager)Forms.Context.GetSystemService(Context.ConnectivityService);
				if (connectivityManager.ActiveNetworkInfo == null)
					return false;
				else
					return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
