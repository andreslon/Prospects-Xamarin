using Prospects.Cross.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Android.Services
{
    public class FileService : IFileService
    {
        public Task<bool> Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(string fileName, bool validateDate = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAppVersion()
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> LoadAsync<TResponse>(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync<T>(string fileName, T content)
        {
            throw new NotImplementedException();
        }
    }
}
