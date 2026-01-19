namespace ECommerce.Application.DTOs.Fatura
{
    public class FaturaListDto
    {
        public int Id { get; set; }
        public string CariUnvan { get; set; } = null!;
        public DateTime FaturaTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
