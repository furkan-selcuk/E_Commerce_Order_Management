using ECommerce.Blazor.Models;

namespace ECommerce.Blazor.Services
{
    public class StokService
    {
        private readonly ApiClient _apiClient;

        public StokService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        //tüm stokları listeler
        public async Task<List<StokListDto>> GetAllAsync()
        {
            var result = await _apiClient.GetAsync<List<StokListDto>>("api/Stok");
            return result ?? new List<StokListDto>();
        }
        // idiye gör stok bilgilerini getirme
        public async Task<StokListDto?> GetByIdAsync(int id)
        {
            var result = await _apiClient.GetAsync<StokListDto>($"api/Stok/{id}");
            return result;
        }
        // yeni stok ekleme
        public async Task AddAsync(StokCreateDto dto)
        {
            await _apiClient.PostAsync("api/Stok", dto);
        }
        // stok güncelleme
        public async Task UpdateAsync(StokUpdateDto dto)
        {
            await _apiClient.PutAsync("api/Stok", dto);
        }
        // stok silme
        public async Task DeleteAsync(int id)
        {
            await _apiClient.DeleteAsync($"api/Stok/{id}");
        }
    }
}
