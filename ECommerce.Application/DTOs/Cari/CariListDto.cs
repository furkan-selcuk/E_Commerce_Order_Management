namespace ECommerce.Application.DTOs.Cari
{
    public class CariListDto
    {
        public int CariId { get; set; }
        public string CariAdi { get; set; } = null!;
        public string? VergiNo { get; set; }
        public string? Telefon { get; set; }
    }
}
