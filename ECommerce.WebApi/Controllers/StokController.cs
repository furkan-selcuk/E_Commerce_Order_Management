using ECommerce.Application.DTOs.Stok;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ECommerce.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StokController : ControllerBase
    {
        private readonly IStokService _stokService;

        public StokController(IStokService stokService)
        {
            _stokService = stokService;
        }
        // tüm stokları getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stokService.GetAllAsync();
            return Ok(result);
        }

        // stok ekler
        [HttpPost]
        public async Task<IActionResult> Create(StokCreateDto dto)
        {
            await _stokService.AddAsync(dto);
            return Ok();
        }

        // stok günceller
        [HttpPut]
        public async Task<IActionResult> Update(StokUpdateDto dto)
        {
            await _stokService.UpdateAsync(dto);
            return Ok();
        }

        // stok siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stokService.DeleteAsync(id);
            return Ok();
        }
    }
}
