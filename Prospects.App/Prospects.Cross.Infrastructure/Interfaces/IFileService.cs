using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task SaveAsync<T>(string fileName, T content);
        Task<TResponse> LoadAsync<TResponse>(string fileName);
        Task<bool> Delete(string fileName);
        Task<bool> Exist(string fileName, bool validateDate = false);
        Task<bool> ExistAppVersion();
    }
}
