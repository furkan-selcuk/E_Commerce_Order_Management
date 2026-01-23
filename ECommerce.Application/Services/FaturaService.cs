using System.Data;
using ECommerce.Application.DTOs.Fatura;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;

namespace ECommerce.Application.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly IFaturaSatirRepository _faturaSatirRepository;
        private readonly IStokRepository _stokRepository;
        private readonly ICariRepository _cariRepository;

        public FaturaService(
            IFaturaRepository faturaRepository,
            IFaturaSatirRepository faturaSatirRepository,
            IStokRepository stokRepository,
            ICariRepository cariRepository)
        {
            _faturaRepository = faturaRepository;
            _faturaSatirRepository = faturaSatirRepository;
            _stokRepository = stokRepository;
            _cariRepository = cariRepository;
        }
        // yeni fatura ekleme
        public async Task<int> CreateAsync(FaturaCreateDto dto)
        {
            var cari = await _cariRepository.GetByIdAsync(dto.CariId);
            if (cari == null)
                throw new BusinessException("Seçilen cari bulunamadı");

            if (!dto.Satirlar.Any())
                throw new BusinessException("Fatura en az bir satır içermelidir");

            var fatura = new Fatura
            {
                CariId = dto.CariId,
                FaturaTarihi = dto.FaturaTarihi
            };

            var satirEntities = dto.Satirlar.Select(s => new FaturaSatir
            {
                StokId = s.StokId,
                Miktar = s.Miktar,
                BirimFiyat = s.BirimFiyat,
                StokAdi = null
            }).ToList();

            try
            {
                return await _faturaRepository.AddWithSatirlarAsync(fatura, satirEntities);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        // tüm faturaları listeleme
        public async Task<IEnumerable<FaturaListDto>> GetAllAsync()
        {
            var faturalar = await _faturaRepository.GetAllWithCariAsync();

            return faturalar.Select(f => new FaturaListDto
            {
                Id = f.Id,
                CariUnvan = f.CariUnvan,
                FaturaTarihi = f.FaturaTarihi,
                ToplamTutar = f.ToplamTutar
            });
        }
        // fatura satırlarını listeleme
        public async Task<IEnumerable<FaturaSatirListDto>> GetFaturaSatirlariAsync(int faturaId)
        {
            var satirlar = await _faturaSatirRepository.GetByFaturaIdAsync(faturaId);

            return satirlar.Select(s => new FaturaSatirListDto
            {
                Id = s.Id,
                FaturaId = s.FaturaId,
                StokId = s.StokId,
                StokAdi = s.StokAdi ?? "Bilinmeyen Stok",
                Miktar = s.Miktar,
                BirimFiyat = s.BirimFiyat,
                Tutar = s.Tutar
            });
        }
    }
}
