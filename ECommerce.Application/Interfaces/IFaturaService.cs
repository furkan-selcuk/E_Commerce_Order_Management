using ECommerce.Application.DTOs.Fatura;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IFaturaService
    {
        Task<int> CreateAsync(FaturaCreateDto dto);
        Task<IEnumerable<FaturaListDto>> GetAllAsync();
    }
}
