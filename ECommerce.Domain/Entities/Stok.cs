namespace ECommerce.Domain.Entities
{
    public class Stok
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
