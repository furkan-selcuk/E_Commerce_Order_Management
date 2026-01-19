using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface IFaturaSatirRepository
    {
        Task AddAsync(FaturaSatir faturaSatir);
        Task<IEnumerable<FaturaSatir>> GetByFaturaIdAsync(int faturaId);
    }
}
