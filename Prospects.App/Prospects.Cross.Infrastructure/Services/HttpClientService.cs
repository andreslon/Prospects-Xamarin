using Newtonsoft.Json;
using Prospects.Cross.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Prospects.Cross.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<HttpResponseMessage> GetAsync(string serviceUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(new Uri(serviceUrl));
            }
        }
        public async Task<HttpResponseMessage> GetAsync(string serviceUrl, Dictionary<string, string> headers = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                return await client.GetAsync(new Uri(serviceUrl));
            }
        }
    }
}
