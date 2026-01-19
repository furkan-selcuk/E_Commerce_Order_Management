using ECommerce.Application.DTOs.Fatura;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ECommerce.WebApi.Controllers
{
    [Authorize]
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

        // fatura ekler
        [HttpPost]
        public async Task<IActionResult> Create(FaturaCreateDto dto)
        {
            var faturaId = await _faturaService.CreateAsync(dto);
            return Ok(new { faturaId });
        }
    }
}
