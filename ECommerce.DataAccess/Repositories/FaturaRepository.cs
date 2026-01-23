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
        // faturanın toplam tutarını güncelleme
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
        // yeni fatura ve satırları ekleme
        public async Task<int> AddWithSatirlarAsync(Fatura fatura, IEnumerable<FaturaSatir> satirlar)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var insertFaturaSql = @"
                    INSERT INTO Faturalar (CariId, FaturaTarihi, ToplamTutar)
                    VALUES (@CariId, @FaturaTarihi, @ToplamTutar);
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                var faturaId = await connection.QuerySingleAsync<int>(insertFaturaSql, new
                {
                    CariId = fatura.CariId,
                    FaturaTarihi = fatura.FaturaTarihi,
                    ToplamTutar = 0m
                }, transaction);

                decimal toplam = 0m;

                var insertSatirSql = @"
                    INSERT INTO FaturaSatirlari (FaturaId, StokId, Miktar, BirimFiyat)
                    VALUES (@FaturaId, @StokId, @Miktar, @BirimFiyat);
                ";

                foreach (var satir in satirlar)
                {
                    
                    var stok = await connection.QueryFirstOrDefaultAsync<int?>(
                        "SELECT StokMiktar FROM Stok WHERE StokId = @StokId", new { StokId = satir.StokId }, transaction);
                    if (stok == null)
                    {
                        throw new InvalidOperationException($"Stok bulunamadı (Id={satir.StokId})");
                    }
                    if (stok.Value < satir.Miktar)
                    {
                        throw new InvalidOperationException($"{satir.StokAdi ?? "Stok"} için yeterli stok yok");
                    }

                    
                    await connection.ExecuteAsync(
                        "UPDATE Stok SET StokMiktar = StokMiktar - @Miktar WHERE StokId = @StokId",
                        new { Miktar = satir.Miktar, StokId = satir.StokId }, transaction);

                   
                    await connection.ExecuteAsync(insertSatirSql, new
                    {
                        FaturaId = faturaId,
                        StokId = satir.StokId,
                        Miktar = satir.Miktar,
                        BirimFiyat = satir.BirimFiyat
                    }, transaction);

                    toplam += satir.Miktar * satir.BirimFiyat;
                }

                
                await connection.ExecuteAsync(
                    "UPDATE Faturalar SET ToplamTutar = @Toplam WHERE Id = @Id",
                    new { Toplam = toplam, Id = faturaId }, transaction);

                transaction.Commit();
                return faturaId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        // idiiye göre fatura bilgilerini getirme
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
        // tüm faturaları listeleme
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
        // fatura ve cari bilgilerini birlikte getirme
        public async Task<IEnumerable<FaturaWithCari>> GetAllWithCariAsync()
        {
            var sql = @"
                SELECT
                    f.Id,
                    f.FaturaTarihi,
                    f.ToplamTutar,
                    c.CariAdi AS CariUnvan
                FROM Faturalar f
                INNER JOIN Cari c ON c.Id = f.CariId
                ORDER BY f.FaturaTarihi DESC
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<FaturaWithCari>(sql);
        }

    }
}
