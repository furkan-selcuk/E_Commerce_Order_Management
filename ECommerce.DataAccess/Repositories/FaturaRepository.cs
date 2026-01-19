using Dapper;
using ECommerce.DataAccess.Context;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.DataAccess.Repositories
{
    public class FaturaRepository : IFaturaRepository
    {
        private readonly DapperContext _context;

        public FaturaRepository(DapperContext context)
        {
            _context = context;
        }
        // faturanın toplam tutarını günceller
        public async Task UpdateTotalAsync(int faturaId, decimal toplamTutar)
        {
            var sql = @"
                UPDATE Faturalar
                SET ToplamTutar = @ToplamTutar
                WHERE Id = @Id
            ";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                Id = faturaId,
                ToplamTutar = toplamTutar
            });
        }
        // yeni fatura ekleme
        public async Task<int> AddAsync(Fatura fatura)
        {
            var sql = @"
                INSERT INTO Faturalar
                (CariId, FaturaTarihi, ToplamTutar)
                VALUES
                (@CariId, @FaturaTarihi, @ToplamTutar);

                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleAsync<int>(sql, fatura);
        }
        // idiiye göre fatura bilgilerini getiri
        public async Task<Fatura?> GetByIdAsync(int faturaId)
        {
            var sql = @"
                SELECT *
                FROM Faturalar
                WHERE Id = @Id
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Fatura>(sql, new { Id = faturaId });
        }
        // tüm faturaları listeler
        public async Task<IEnumerable<Fatura>> GetAllAsync()
        {
            var sql = @"
                SELECT *
                FROM Faturalar
                ORDER BY FaturaTarihi DESC
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Fatura>(sql);
        }
        // fatura ve cari bilgilerini birlikte getirir
        public async Task<IEnumerable<FaturaWithCari>> GetAllWithCariAsync()
        {
            var sql = @"
                SELECT
                    f.Id,
                    f.FaturaTarihi,
                    f.ToplamTutar,
                    c.Unvan AS CariUnvan
                FROM Faturalar f
                INNER JOIN Cariler c ON c.Id = f.CariId
                ORDER BY f.FaturaTarihi DESC
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<FaturaWithCari>(sql);
        }

    }
}
