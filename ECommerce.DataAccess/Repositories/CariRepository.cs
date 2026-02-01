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
        // tüm carileri listeleme
        public async Task<IEnumerable<Cari>> GetAllAsync()
        {
            const string query = @"
                    SELECT 
                    Id,
                    CariKodu,
                    CariAdi,
                    VergiNo,
                    Telefon,
                    Email,
                    Adres,
                    OlusturmaTarihi
                FROM Cari
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Cari>(query);
        }
        // idiye göre cari bilgilerini getirme
        public async Task<Cari?> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT *
                FROM Cari
                WHERE Id = @Id
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Cari>(
                query,
                new { Id = id }
            );
        }

        // yeni cari ekleme
        public async Task AddAsync(Cari cari)
        {
            const string insertQuery = @"
                INSERT INTO Cari
                (CariKodu, CariAdi, VergiNo, Telefon, Email, Adres, OlusturmaTarihi)
                VALUES
                (@CariKodu, @CariAdi, @VergiNo, @Telefon, @Email, @Adres, @OlusturmaTarihi);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            using var connection = _context.CreateConnection(); // 🔥 EKSİK OLAN BUYDU

            var newId = await connection.ExecuteScalarAsync<int>(insertQuery, cari);
            cari.Id = newId;
        }
        // cari güncelleme
        public async Task UpdateAsync(Cari cari)
        {
            const string query = @"
                UPDATE Cari
                SET CariKodu = @CariKodu,
                    CariAdi = @CariAdi,
                    VergiNo = @VergiNo,
                    Telefon = @Telefon,
                    Email = @Email,
                    Adres = @Adres
                WHERE Id = @Id
            ";

            using var connection = _context.CreateConnection();
            try
            {
                Console.WriteLine($"[CariRepository] UpdateAsync called for Id={cari.Id}, CariKodu={cari.CariKodu}");
            }
            catch { }
            await connection.ExecuteAsync(query, cari);
        }
        // cari koduna göre cari var mı kontrolü
        public async Task<bool> ExistsByCariKodu(string cariKodu)
        {
            const string query = @"SELECT COUNT(1) FROM Cari WHERE CariKodu = @CariKodu";

            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(
                query,
                new { CariKodu = cariKodu }
            );

            return count > 0;
        }
        // cari silme
        public async Task DeleteAsync(int cariId)
        {

            const string query = @"DELETE FROM Cari
                                     WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = cariId });
        }
    }
}
