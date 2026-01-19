namespace ECommerce.Domain.Entities
{
    public class Fatura
    {
        public int Id { get; set; }
        public int CariId { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
