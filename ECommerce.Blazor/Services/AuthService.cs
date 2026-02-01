namespace ECommerce.Blazor.Services
{
    public class AuthService
    {
        private readonly ApiClient _apiClient;

        public AuthService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var response = await _apiClient.PostAsync<object, LoginResponse>($"api/Auth/login?username={username}&password={password}", new { });
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    _apiClient.SetToken(response.Token);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private class LoginResponse
        {
            public string Token { get; set; } = null!;
        }
    }
}
