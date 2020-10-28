using CodePlus.Blazor.Data.Ocelots.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Data.Https
{
    public class HttpUtilityService : IHttpUtilityService
    {
        private readonly HttpClient _httpClient;

        public string HttpClientName { get; set; } = AppConsts.DefaultHttpClient;

        public HttpUtilityService(IHttpClientFactory httpFactory)
        {
            _httpClient = httpFactory.CreateClient(HttpClientName);
        }

        public async Task<T> PostAsync<T>(string uri, object data = null, string token = null) where T : class
        {
            _httpClient.DefaultRequestHeaders.Clear();
            var jsonData = JsonConvert.SerializeObject(data);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(jsonData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            var response = await _httpClient.PostAsync(uri, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string readAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(readAsString);
                return result;
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public async Task<T> GetAsync<T>(string uri, string token = null) where T : class
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string readAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(readAsString);
                return result;
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public async Task<T> PostFormDataAsync<T>(string uri, Dictionary<string,string> data = null) where T : class
        {
            HttpContent httpContent = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(uri, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string readAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(readAsString);
                return result;
            }
            else
            {
                throw new ApplicationException();
            }
        }
    }
}


