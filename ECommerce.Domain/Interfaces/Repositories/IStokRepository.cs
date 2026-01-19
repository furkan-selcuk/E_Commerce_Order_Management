using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface IStokRepository
    {
        Task<IEnumerable<Stok>> GetAllAsync();
        Task<Stok?> GetByIdAsync(int stokId);
        Task AddAsync(Stok stok);
        Task UpdateAsync(Stok stok);
        Task DeleteAsync(int stokId);
    }
}
