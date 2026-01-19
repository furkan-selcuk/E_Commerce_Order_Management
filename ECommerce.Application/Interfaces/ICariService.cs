using ECommerce.Application.DTOs.Cari;

namespace ECommerce.Application.Interfaces.Services
{
    public interface ICariService
    {
        Task<IEnumerable<CariListDto>> GetAllAsync();
        Task AddAsync(CariCreateDto dto);
        Task UpdateAsync(CariUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
