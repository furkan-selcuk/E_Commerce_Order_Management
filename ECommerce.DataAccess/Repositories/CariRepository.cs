using Dapper;
using ECommerce.DataAccess.Context;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.DataAccess.Repositories
{
    public class CariRepository : ICariRepository
    {
        private readonly DapperContext _context;

        public CariRepository(DapperContext context)
        {
            _context = context;
        }
        // tüm carileri listeler
        public async Task<IEnumerable<Cari>> GetAllAsync()
        {
            const string query = @"SELECT 
                                        CariId,
                                        CariAdi,
                                        VergiNo,
                                        Telefon
                                   FROM Cari";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Cari>(query);
        }
        // idiye göre cari bilgilerini getirir
        public async Task<Cari?> GetByIdAsync(int cariId)
        {
            const string query = @"SELECT 
                                        CariId,
                                        CariAdi,
                                        VergiNo,
                                        Telefon
                                   FROM Cari
                                   WHERE CariId = @CariId";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Cari>(
                query,
                new { CariId = cariId }
            );
        }
        // yeni cari ekleme
        public async Task AddAsync(Cari cari)
        {
            const string query = @"INSERT INTO Cari
                                   (CariAdi, VergiNo, Telefon)
                                   VALUES
                                   (@CariAdi, @VergiNo, @Telefon)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, cari);
        }
        // cari güncelleme
        public async Task UpdateAsync(Cari cari)
        {
            const string query = @"UPDATE Cari
                                   SET CariAdi = @CariAdi,
                                       VergiNo = @VergiNo,
                                       Telefon = @Telefon
                                   WHERE CariId = @CariId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, cari);
        }
        // cari silme
        public async Task DeleteAsync(int cariId)
        {
            const string query = @"DELETE FROM Cari
                                   WHERE CariId = @CariId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { CariId = cariId });
        }
    }
}
