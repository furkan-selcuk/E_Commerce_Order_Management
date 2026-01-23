namespace ECommerce.Application.DTOs.Cari
{
    public class CariUpdateDto
    {
        public int Id { get; set; }
        public string? CariKodu { get; set; }
        public string CariAdi { get; set; } = null!;
        public string? VergiNo { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
    }
}
