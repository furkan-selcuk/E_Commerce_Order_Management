using Dapper;
using ECommerce.DataAccess.Context;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using System.Data;

namespace ECommerce.DataAccess.Repositories
{
    public class FaturaSatirRepository : IFaturaSatirRepository
    {
        private readonly DapperContext _context;

        public FaturaSatirRepository(DapperContext context)
        {
            _context = context;
        }
        // yeni fatura satırı ekleme
        public async Task AddAsync(FaturaSatir faturaSatir)
        {
            var sql = @"
                INSERT INTO FaturaSatirlari
                (FaturaId, StokId, Miktar, BirimFiyat)
                VALUES
                (@FaturaId, @StokId, @Miktar, @BirimFiyat)
            ";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, faturaSatir);
        }
        // faturaya ait satırları listeleme
        public async Task<IEnumerable<FaturaSatir>> GetByFaturaIdAsync(int faturaId)
        {
            var sql = @"
                SELECT fs.*, s.StokAdi
                FROM FaturaSatirlari fs
                LEFT JOIN Stok s ON s.StokId = fs.StokId
                WHERE fs.FaturaId = @FaturaId
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<FaturaSatir>(sql, new { FaturaId = faturaId });
        }
    }
}
