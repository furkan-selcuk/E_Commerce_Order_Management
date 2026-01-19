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
                Id = s.Id,
                Ad = s.Ad,
                Miktar = s.Miktar,
                Fiyat = s.Fiyat
            });
        }
        // yeni stok ekleme
        public async Task AddAsync(StokCreateDto dto)
        {
            if (dto.Miktar < 0)
                throw new BusinessException("Stok miktar negatif olamaz");

            var stok = new Stok
            {
                Ad = dto.Ad,
                Miktar = dto.Miktar,
                Fiyat = dto.Fiyat
            };

            await _stokRepository.AddAsync(stok);
        }
        // stok güncelleme
        public async Task UpdateAsync(StokUpdateDto dto)
        {
            var stok = await _stokRepository.GetByIdAsync(dto.Id);
            if (stok == null)
                throw new NotFoundException("Stok bulunamad");

            stok.Ad = dto.Ad;
            stok.Miktar = dto.Miktar;
            stok.Fiyat = dto.Fiyat;

            await _stokRepository.UpdateAsync(stok);
        }
        // stok silme
        public async Task DeleteAsync(int id)
        {
            await _stokRepository.DeleteAsync(id);
        }
    }
}
