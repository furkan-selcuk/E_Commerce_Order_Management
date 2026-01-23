using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace ECommerce.Blazor.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private string? _token;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ECommerceApi");
        }
        //token ayarlar
        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        public bool HasToken() => !string.IsNullOrEmpty(_token);
        
        public async Task<T?> GetAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }

        public async Task PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task PutAsync<T>(string url, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status {(int)response.StatusCode}: {content}");
            }
        }
    }
}
