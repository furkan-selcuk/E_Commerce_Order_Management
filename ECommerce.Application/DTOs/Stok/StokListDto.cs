namespace ECommerce.Application.DTOs.Stok
{
    public class StokListDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
