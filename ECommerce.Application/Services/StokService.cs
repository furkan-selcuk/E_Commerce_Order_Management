using ECommerce.Application.DTOs.Stok;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.Application.Services
{
    public class StokService : IStokService
    {
        private readonly IStokRepository _stokRepository;

        public StokService(IStokRepository stokRepository)
        {
            _stokRepository = stokRepository;
        }
        //tüm stokları listeler
        public async Task<IEnumerable<StokListDto>> GetAllAsync()
        {
            var stoklar = await _stokRepository.GetAllAsync();
 
            return stoklar.Select(s => new StokListDto
            {
                StokId = s.StokId,
                StokKodu = s.StokKodu,
                StokAdi = s.StokAdi,
                StokMiktar = s.StokMiktar,
                BirimFiyat = s.BirimFiyat
            });
        }
        // idiye gör stok bilgilerini getirme
        public async Task<StokListDto?> GetByIdAsync(int id)
        {
            var s = await _stokRepository.GetByIdAsync(id);
            if (s == null) return null;
            return new StokListDto
            {
                StokId = s.StokId,
                StokKodu = s.StokKodu,
                StokAdi = s.StokAdi,
                StokMiktar = s.StokMiktar,
                BirimFiyat = s.BirimFiyat
            };
        }
        // yeni stok ekleme
        public async Task AddAsync(StokCreateDto dto)
        {
            if (dto.StokMiktar < 0)
                throw new BusinessException("Stok miktar negatif olamaz");
 
            var stok = new Stok
            {
                StokKodu = dto.StokKodu,
                StokAdi = dto.StokAdi,
                StokMiktar = dto.StokMiktar,
                BirimFiyat = dto.BirimFiyat
            };
 
            await _stokRepository.AddAsync(stok);
        }
        // stok güncelleme
        public async Task UpdateAsync(StokUpdateDto dto)
        {
            var stok = await _stokRepository.GetByIdAsync(dto.StokId);
            if (stok == null)
                throw new NotFoundException("Stok bulunamadı");
 
            stok.StokKodu = dto.StokKodu;
            stok.StokAdi = dto.StokAdi;
            stok.StokMiktar = dto.StokMiktar;
            stok.BirimFiyat = dto.BirimFiyat;
 
            await _stokRepository.UpdateAsync(stok);
        }
        // stok silme
        public async Task DeleteAsync(int id)
        {
            await _stokRepository.DeleteAsync(id);
        }
    }
}
