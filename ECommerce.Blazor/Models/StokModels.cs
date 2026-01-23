using System;

namespace ECommerce.Blazor.Models
{
    public class StokListDto
    {
        public int StokId { get; set; }
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public int StokMiktar { get; set; }
        public decimal BirimFiyat { get; set; }
    }

    public class StokCreateDto
    {
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public int StokMiktar { get; set; }
        public decimal BirimFiyat { get; set; }
    }

    public class StokUpdateDto
    {
        public int StokId { get; set; }
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public int StokMiktar { get; set; }
        public decimal BirimFiyat { get; set; }
    }
}
