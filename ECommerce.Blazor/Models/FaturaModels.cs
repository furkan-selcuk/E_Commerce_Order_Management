using System;
using System.Collections.Generic;

namespace ECommerce.Blazor.Models
{
    public class FaturaListDto
    {
        public int Id { get; set; }
        public string CariUnvan { get; set; } = null!;
        public DateTime FaturaTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
    }

    public class FaturaCreateDto
    {
        public int CariId { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public List<FaturaSatirCreateDto> Satirlar { get; set; } = new();
    }

    public class FaturaSatirCreateDto
    {
        public int StokId { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
    }

    public class FaturaSatirListDto
    {
        public int Id { get; set; }
        public int FaturaId { get; set; }
        public int StokId { get; set; }
        public string StokAdi { get; set; } = null!;
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
    }
}
