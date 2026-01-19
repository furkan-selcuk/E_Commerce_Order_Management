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
        // tüm carileri listeler
        public async Task<IEnumerable<CariListDto>> GetAllAsync()
        {
            var cariler = await _cariRepository.GetAllAsync();

            return cariler.Select(c => new CariListDto
            {
                CariId = c.CariId,
                CariAdi = c.CariAdi,
                VergiNo = c.VergiNo,
                Telefon = c.Telefon
            });
        }
        // yeni cari ekleme
        public async Task AddAsync(CariCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CariAdi))
                throw new BusinessException("Cari adı boş olamaz");

            var cari = new Cari
            {
                CariAdi = dto.CariAdi,
                VergiNo = dto.VergiNo,
                Telefon = dto.Telefon
            };

            await _cariRepository.AddAsync(cari);
        }
        // cari güncelleme
        public async Task UpdateAsync(CariUpdateDto dto)
        {
            var cari = await _cariRepository.GetByIdAsync(dto.CariId);
            if (cari == null)
                throw new NotFoundException("Cari bulunamadı");

            cari.CariAdi = dto.CariAdi;
            cari.VergiNo = dto.VergiNo;
            cari.Telefon = dto.Telefon;

            await _cariRepository.UpdateAsync(cari);
        }
        // cari silme
        public async Task DeleteAsync(int id)
        {
            await _cariRepository.DeleteAsync(id);
        }
    }
}
