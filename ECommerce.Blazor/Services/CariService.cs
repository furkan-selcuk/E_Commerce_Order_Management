using ECommerce.Blazor.Models;

namespace ECommerce.Blazor.Services
{
    public class CariService
    {
        private readonly ApiClient _apiClient;

        public CariService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        //tüm carileri listeler
        public async Task<List<CariListDto>> GetAllAsync()
        {
            var result = await _apiClient.GetAsync<List<CariListDto>>("api/Cari");
            return result ?? new List<CariListDto>();
        }
        // idiye gör cari bilgilerini getirme
        public async Task<CariUpdateDto?> GetByIdAsync(int id)
        {
            return await _apiClient.GetAsync<CariUpdateDto>($"api/Cari/{id}");
        }
        // yeni cari ekleme
        public async Task AddAsync(CariCreateDto dto)
        {
            await _apiClient.PostAsync("api/Cari", dto);
        }
        // cari güncelleme
        public async Task UpdateAsync(CariUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            await _apiClient.PutAsync($"api/Cari/{dto.Id}", dto);
        }
        // cari silme
        public async Task DeleteAsync(int id)
        {
            await _apiClient.DeleteAsync($"api/Cari/{id}");
        }
    }
}
