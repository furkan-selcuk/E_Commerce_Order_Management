using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface ICariRepository
    {
        Task<IEnumerable<Cari>> GetAllAsync();
        Task<Cari?> GetByIdAsync(int cariId);
        Task AddAsync(Cari cari);
        Task UpdateAsync(Cari cari);
        Task DeleteAsync(int cariId);
    }
}
