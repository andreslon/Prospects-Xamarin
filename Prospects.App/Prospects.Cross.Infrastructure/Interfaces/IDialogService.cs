using System;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);
        Task<bool> ShowConfirm(string message, string title);
    }
}
