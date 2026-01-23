using ECommerce.Application.DTOs.Fatura;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ECommerce.WebApi.Controllers
{
    //[Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _faturaService;

        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }
        // tüm faturaları getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _faturaService.GetAllAsync();
            return Ok(result);
        }
        // tek bir faturayı getirir
        [HttpPost]
        public async Task<IActionResult> Create(FaturaCreateDto dto)
        {
            try
            {
                var faturaId = await _faturaService.CreateAsync(dto);
                return Ok(new { faturaId });
            }
            catch (Exception ex)
            {

                Console.WriteLine("🔥 Fatura Create ERROR:");
                Console.WriteLine(ex.ToString());

                return StatusCode(500, new
                {
                    error = ex.Message,
                    detail = ex.InnerException?.Message
                });
            }
        }

        // faturaya ait satırları getirir
        [HttpGet("{id}/satirlar")]
        public async Task<IActionResult> GetSatirlar(int id)
        {
            var result = await _faturaService.GetFaturaSatirlariAsync(id);
            return Ok(result);
        }
    }
}
