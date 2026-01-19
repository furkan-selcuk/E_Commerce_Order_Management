namespace ECommerce.Domain.Entities
{
    public class FaturaSatir
    {
        public int Id { get; set; }
        public int FaturaId { get; set; }
        public int StokId { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
    }
}
