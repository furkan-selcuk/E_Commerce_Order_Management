using ECommerce.Application.DTOs.Cari;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.Application.Services
{
    public class CariService : ICariService
    {
        private readonly ICariRepository _cariRepository;

        public CariService(ICariRepository cariRepository)
        {
            _cariRepository = cariRepository;
        }
        // tüm carileri listeme
        public async Task<IEnumerable<CariListDto>> GetAllAsync()
        {
            var cariler = await _cariRepository.GetAllAsync();

            return cariler.Select(c => new CariListDto
            {
                Id = c.Id,
                CariKodu = c.CariKodu,
                CariAdi = c.CariAdi,
                VergiNo = c.VergiNo,
                Telefon = c.Telefon,
                Email = c.Email,
                Adres = c.Adres,
                OlusturmaTarihi = c.OlusturmaTarihi
            });
        }
        // idiye göre cari bilgilerini getirme
        public async Task<CariUpdateDto?> GetByIdAsync(int id)
        {
            var c = await _cariRepository.GetByIdAsync(id);
            if (c == null) return null;

            return new CariUpdateDto
            {
                Id = c.Id,
                CariKodu = c.CariKodu,
                CariAdi = c.CariAdi,
                VergiNo = c.VergiNo,
                Telefon = c.Telefon,
                Email = c.Email,
                Adres = c.Adres
            };
        }

        // yeni cari ekleme
        public async Task AddAsync(CariCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CariAdi))
                throw new BusinessException("Cari adı boş olamaz");
            var exists = await _cariRepository.ExistsByCariKodu(dto.CariKodu);
            if (exists)
                throw new BusinessException("Bu cari kodu zaten mevcut");
            var cari = new Cari
            {
                CariKodu = dto.CariKodu,
                CariAdi = dto.CariAdi,
                VergiNo = dto.VergiNo,
                Telefon = dto.Telefon,
                Email = dto.Email,
                Adres = dto.Adres,
                OlusturmaTarihi = DateTime.Now
            };

            await _cariRepository.AddAsync(cari);
        }
        // cari güncelleme
        public async Task UpdateAsync(CariUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CariAdi))
                throw new BusinessException("Cari adı boş olamaz");
            var cari = await _cariRepository.GetByIdAsync(dto.Id);
            if (cari == null)
                throw new NotFoundException("Cari bulunamadı");

            cari.CariKodu = dto.CariKodu;
            cari.CariAdi = dto.CariAdi;
            cari.VergiNo = dto.VergiNo;
            cari.Telefon = dto.Telefon;
            cari.Email = dto.Email;
            cari.Adres = dto.Adres;

            await _cariRepository.UpdateAsync(cari);
        }
        // cari silme
        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Id geçersiz");

            var cari = await _cariRepository.GetByIdAsync(id);
            if (cari == null)
                throw new NotFoundException("Cari bulunamadı");

            await _cariRepository.DeleteAsync(id);
        }
    }
}
