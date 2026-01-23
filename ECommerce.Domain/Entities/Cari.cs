namespace ECommerce.Domain.Entities
{
    public class Cari
    {
        public int Id { get; set; }
        public string? CariKodu { get; set; }
        public string CariAdi { get; set; } = null!;
        public string? VergiNo { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }
    }
}
