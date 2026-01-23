namespace ECommerce.Application.DTOs.Stok
{
    public class StokCreateDto
    {
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public decimal BirimFiyat { get; set; }
        public int StokMiktar { get; set; }
    }
}
