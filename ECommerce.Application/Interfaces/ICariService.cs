using ECommerce.Application.DTOs.Cari;

namespace ECommerce.Application.Interfaces.Services
{
    public interface ICariService
    {
        Task<IEnumerable<CariListDto>> GetAllAsync();
        Task<CariUpdateDto?> GetByIdAsync(int id);
        Task AddAsync(CariCreateDto dto);
        Task UpdateAsync(CariUpdateDto dto); 
        Task DeleteAsync(int id);
    }
}
