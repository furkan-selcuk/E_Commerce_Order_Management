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
                throw new BusinessException("seçilen cari bulunamadı");

            if (!dto.Satirlar.Any())
                throw new BusinessException("Fatura enn az bir satır içermelidir");

            decimal toplamTutar = 0;

            var fatura = new Fatura
            {
                CariId = dto.CariId,
                FaturaTarihi = dto.FaturaTarihi
            };
            
            var faturaId = await _faturaRepository.AddAsync(fatura);

            foreach (var satirDto in dto.Satirlar)
            {
                var stok = await _stokRepository.GetByIdAsync(satirDto.StokId);
                if (stok == null)
                    throw new BusinessException("Stok bulunamadı.");

                if (stok.Miktar < satirDto.Miktar)
                    throw new BusinessException($"{stok.Ad} için yeterli stok yok");

                
                stok.Miktar -= satirDto.Miktar;
                await _stokRepository.UpdateAsync(stok);

                var satirTutar = satirDto.Miktar * satirDto.BirimFiyat;
                toplamTutar += satirTutar;

                var faturaSatir = new FaturaSatir
                {
                    FaturaId = faturaId,
                    StokId = satirDto.StokId,
                    Miktar = satirDto.Miktar,
                    BirimFiyat = satirDto.BirimFiyat,
                    Tutar = satirTutar
                };

                await _faturaSatirRepository.AddAsync(faturaSatir);
            }

            fatura.Id = faturaId;
            fatura.ToplamTutar = toplamTutar;
            await _faturaRepository.UpdateTotalAsync(faturaId, toplamTutar);

            return faturaId;
        }
        // tüm faturaları listeler
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
    }
}
