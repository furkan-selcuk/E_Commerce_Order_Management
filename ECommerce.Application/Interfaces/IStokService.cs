using ECommerce.Application.DTOs.Stok;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IStokService
    {
        Task<IEnumerable<StokListDto>> GetAllAsync();
        Task<StokListDto?> GetByIdAsync(int id);
        Task AddAsync(StokCreateDto dto);
        Task UpdateAsync(StokUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
