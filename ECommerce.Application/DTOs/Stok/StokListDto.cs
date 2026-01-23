namespace ECommerce.Application.DTOs.Stok
{
    public class StokListDto
    {
        public int StokId { get; set; }
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public decimal BirimFiyat { get; set; }
        public int StokMiktar { get; set; }
    }
}
