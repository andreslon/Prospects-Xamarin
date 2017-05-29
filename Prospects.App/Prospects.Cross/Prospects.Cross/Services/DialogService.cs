using System;
using System.Threading.Tasks;
using Prospects.Cross.Infrastructure.Interfaces;

namespace Prospects.Cross.Services
{
    public class DialogService : IDialogService
    {
		public async Task ShowMessage(string message, string title)
		{
			await App.NavigationPage.DisplayAlert(title, message, "Aceptar");
		} 
		public async Task<bool> ShowConfirm(string message, string title)
		{
			return await App.NavigationPage.DisplayAlert(title, message, "Sí", "No");
		} 
    }
}