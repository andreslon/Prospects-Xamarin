using Newtonsoft.Json;
using Prospects.Cross.Infrastructure.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Services
{
    public class JsonService : IJsonService
    {
        public async Task<TResponse> GetSerializedResponse<TResponse>(HttpResponseMessage result)
        {
            string response = await result.Content.ReadAsStringAsync();
            TResponse serializedResponse = JsonConvert.DeserializeObject<TResponse>(response);
            return serializedResponse;
        }


        public T Deserialize<T>(string text)
        {
            T deserializedObject = JsonConvert.DeserializeObject<T>(text);
            return deserializedObject;
        }
        public string Serialize(object obj)
        {
            string serializedObject = JsonConvert.SerializeObject(obj);
            return serializedObject;
        }
    }
}
