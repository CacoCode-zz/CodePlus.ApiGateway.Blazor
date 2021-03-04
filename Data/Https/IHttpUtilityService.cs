using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodePlus.ApiGateway.Data.Https
{
    public interface IHttpUtilityService
    {
        string HttpClientName { get; set; }

        Task<HttpResponseResult<T>> PostAsync<T>(string uri, object data = null, string token = null) where T : class;

        Task<T> GetAsync<T>(string uri, string token = null) where T : class;

        Task<T> PostFormDataAsync<T>(string uri, Dictionary<string, string> data = null) where T : class;
    }
}
