using Prospects.Cross.Infrastructure.Enumerations;
using System.Threading.Tasks;
namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface INavigationService
    {
        Task Navigate(PageTypes viewName);
        void GoBack();
    }
}
