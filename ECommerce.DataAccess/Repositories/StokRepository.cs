using Dapper;
using ECommerce.DataAccess.Context;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.DataAccess.Repositories
{
    public class StokRepository : IStokRepository
    {
        private readonly DapperContext _context;

        public StokRepository(DapperContext context)
        {
            _context = context;
        }
        // tüm stokları listeler    
        public async Task<IEnumerable<Stok>> GetAllAsync()
        {
            const string query = @"SELECT 
                                        StokId,
                                        StokKodu,
                                        StokAdi,
                                        BirimFiyat,
                                        StokMiktar
                                   FROM Stok";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Stok>(query);
        }
        // idiye göre stok bilgilerini getirir
        public async Task<Stok?> GetByIdAsync(int stokId)
        {
            const string query = @"SELECT 
                                        StokId,
                                        StokKodu,
                                        StokAdi,
                                        BirimFiyat,
                                        StokMiktar
                                   FROM Stok
                                   WHERE StokId = @StokId";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Stok>(
                query,
                new { StokId = stokId }
            );
        }
        // yeni stok ekleme
        public async Task AddAsync(Stok stok)
        {
            const string query = @"INSERT INTO Stok
                                   (StokKodu, StokAdi, BirimFiyat, StokMiktar)
                                   VALUES
                                   (@StokKodu, @StokAdi, @BirimFiyat, @StokMiktar)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, stok);
        }
        // stok güncelleme
        public async Task UpdateAsync(Stok stok)
        {
            const string query = @"UPDATE Stok
                                   SET StokKodu = @StokKodu,
                                       StokAdi = @StokAdi,
                                       BirimFiyat = @BirimFiyat,
                                       StokMiktar = @StokMiktar
                                   WHERE StokId = @StokId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, stok);
        }
        // stok silme   
        public async Task DeleteAsync(int stokId)
        {
            const string query = @"DELETE FROM Stok
                                   WHERE StokId = @StokId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { StokId = stokId });
        }
    }
}
