namespace ECommerce.Application.DTOs.Fatura
{
    public class FaturaCreateDto
    {
        public int CariId { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public List<FaturaSatirCreateDto> Satirlar { get; set; } = new();
    }
}
