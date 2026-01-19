using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface IFaturaRepository
    {
        Task<int> AddAsync(Fatura fatura);
        Task<Fatura?> GetByIdAsync(int faturaId);
        Task<IEnumerable<Fatura>> GetAllAsync();
        Task UpdateTotalAsync(int faturaId, decimal toplamTutar);
        Task<IEnumerable<FaturaWithCari>> GetAllWithCariAsync();
    }
}
