namespace ECommerce.Application.DTOs.Stok
{
    public class StokUpdateDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
