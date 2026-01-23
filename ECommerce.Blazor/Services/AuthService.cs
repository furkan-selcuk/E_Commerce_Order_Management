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
                // AuthController expects query params or form data? 
                // Let's check AuthController again.
                // [HttpPost("login")] public IActionResult Login(string username, string password)
                // This usually means [FromQuery] or [FromForm] in newer ASP.NET Core if not specified.
                // But if it's string username, it's likely [FromQuery].
                
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
