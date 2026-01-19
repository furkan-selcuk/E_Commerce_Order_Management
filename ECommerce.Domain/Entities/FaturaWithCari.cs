namespace ECommerce.Domain.Entities
{
    public class FaturaWithCari
    {
        public int Id { get; set; }
        public string CariUnvan { get; set; } = null!;
        public DateTime FaturaTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
