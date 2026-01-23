namespace ECommerce.Application.DTOs.Fatura
{
    public class FaturaSatirListDto
    {
        public int Id { get; set; }
        public int FaturaId { get; set; }
        public int StokId { get; set; }
        public string StokAdi { get; set; } = null!;
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
    }
}
