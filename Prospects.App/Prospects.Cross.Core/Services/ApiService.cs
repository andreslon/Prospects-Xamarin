using Prospects.Cross.Infrastructure.Interfaces;
using Prospects.Cross.Infrastructure.Models.Reponses;
using Prospects.Cross.Infrastructure.Models.Responses;
using Prospects.Cross.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Prospects.Cross.Core.ViewModels;
using Prospects.Cross.Infrastructure;

namespace Prospects.Cross.Core.Services
{
    public class ApiService : IApiService
    {

        public IHttpClientService HttpClientService { get; set; }
        public IJsonService JsonService { get; set; }

        public string apiUri { get; set; } = LocalizedStrings.Get("ApiUri");

        public ApiService(IJsonService jsonService, IHttpClientService httpClientService)
        {
            JsonService = jsonService;
            HttpClientService = httpClientService;
        }
        public async Task<BaseResponse<AuthResponse>> Login(string email, string password)
        {
            try
            {
                HttpResponseMessage result = await HttpClientService.GetAsync($"{apiUri}application/login?email={email}&password={password}");
                var serializedResponse = await JsonService.GetSerializedResponse<AuthResponse>(result);
                var response = new BaseResponse<AuthResponse>() { Response = serializedResponse };
                response.HttpResponse = result;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<AuthResponse>() { HttpResponse = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest } };
            }
        }
        public async Task<BaseResponse<List<ProspectResponse>>> GetProspects(string token)
        {
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("TOKEN", token);
                HttpResponseMessage result = await HttpClientService.GetAsync($"{apiUri}sch/prospects.json", headers);
                if (result.IsSuccessStatusCode)
                {
                    var serializedResponse = await JsonService.GetSerializedResponse<List<ProspectResponse>>(result);
                    var response = new BaseResponse<List<ProspectResponse>>() { Response = serializedResponse };
                    response.HttpResponse = result;
                    return response;
                }
                else
                {
                    return new BaseResponse<List<ProspectResponse>>() { HttpResponse = result };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<ProspectResponse>>() { HttpResponse = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest } };
            }
        }

    }
}
