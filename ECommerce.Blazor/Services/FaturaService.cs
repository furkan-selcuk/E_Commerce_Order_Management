using ECommerce.Blazor.Models;

namespace ECommerce.Blazor.Services
{
    public class FaturaService
    {
        private readonly ApiClient _apiClient;

        public FaturaService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        //tüm faturaları listeler
        public async Task<List<FaturaListDto>> GetAllAsync()
        {
            var result = await _apiClient.GetAsync<List<FaturaListDto>>("api/Fatura");
            return result ?? new List<FaturaListDto>();
        }
        // yeni fatura ekleme
        public async Task<int> CreateAsync(FaturaCreateDto dto)
        {
            var response = await _apiClient.PostAsync<FaturaCreateDto, FaturaCreateResponse>("api/Fatura", dto);
            return response?.FaturaId ?? 0;
        }
        // fatura satırlarını getirme
        public async Task<List<FaturaSatirListDto>> GetSatirlarAsync(int faturaId)
        {
            var result = await _apiClient.GetAsync<List<FaturaSatirListDto>>($"api/Fatura/{faturaId}/satirlar");
            return result ?? new List<FaturaSatirListDto>();
        }
        // fatura satırı ekleme
        private class FaturaCreateResponse
        {
            public int FaturaId { get; set; }
        }
    }
}
