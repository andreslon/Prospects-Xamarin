
using Prospects.Cross.Infrastructure.Interfaces;
using System.Threading.Tasks;
using Prospects.Cross.Infrastructure.Enumerations;
using System;
using Prospects.Cross.Views;

namespace Prospects.Cross.Services
{
	public class NavigationService : INavigationService
	{

		async public Task Navigate(PageTypes viewName)
		{
			try
			{ 
				if (App.MasterDetailPage != null)
				{
					//App.masterDetailPage.IsPresented = false;
				}

				switch (viewName)
				{
					case PageTypes.Splash:
						App.SetSplashPage();
						break;
					case PageTypes.Login:
						App.SetLoginPage();
						break;
					case PageTypes.Home:
						App.SetProspectsPage();
						break;
                    case PageTypes.Log:
                        App.SetPushPage(new LogPage());
                        break;
                    case PageTypes.EditProspect:
						App.SetPushPage(new EditProspectPage());
						break;
                    case PageTypes.ProspectDetail:
                        App.SetPushPage(new ProspectDetailPage());
                        break;

                }
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debugger.Break();
			}

		}

		public async void GoBack()
		{
			await App.NavigationPage.PopAsync();
		}
	}
}
