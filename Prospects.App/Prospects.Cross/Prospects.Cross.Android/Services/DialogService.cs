using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Prospects.Cross.Infrastructure.Interfaces;

namespace Prospects.Cross.Droid.Services
{
    public class DialogService : IDialogService
    {
        public Task<bool> ShowConfirm(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title)
        {
            throw new NotImplementedException();
        }
    }
}