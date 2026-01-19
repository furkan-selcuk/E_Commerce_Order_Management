namespace ECommerce.Application.DTOs.Stok
{
    public class StokCreateDto
    {
        public string Ad { get; set; } = null!;
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
