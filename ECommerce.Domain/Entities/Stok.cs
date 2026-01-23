namespace ECommerce.Domain.Entities
{
    public class Stok
    {
        public int StokId { get; set; }
        public string? StokKodu { get; set; }
        public string StokAdi { get; set; } = null!;
        public decimal BirimFiyat { get; set; }
        public int StokMiktar { get; set; }
    }
}
