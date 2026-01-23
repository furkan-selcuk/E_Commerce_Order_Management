using ECommerce.Application.DTOs.Cari;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CariController : ControllerBase
    {
        private readonly ICariService _cariService;

        public CariController(ICariService cariService)
        {
            _cariService = cariService;
        }
        // tüm carileri getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cariService.GetAllAsync();
            return Ok(result);
        }
        // tek bir cariyi getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cariService.GetByIdAsync(id);
            return Ok(result);
        }

        // cari ekler
        [HttpPost]
        public async Task<IActionResult> Create(CariCreateDto dto)
        {
            await _cariService.AddAsync(dto);
            return Ok();
        }
        //cari günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CariUpdateDto dto)
        {
            if (dto == null) return BadRequest();
            if (id != dto.Id) return BadRequest("Route id and body CariId must match.");

            Console.WriteLine($"[CariController] Update called for CariId={dto.Id}, CariKodu={dto.CariKodu}");

            await _cariService.UpdateAsync(dto);
            return Ok();
        }
        // cari siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cariService.DeleteAsync(id);
            return Ok();
        }
    }
}
