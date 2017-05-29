using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string serviceUrl);
        Task<HttpResponseMessage> GetAsync(string serviceUrl, Dictionary<string, string> headers = null);

    }
}
