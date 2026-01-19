using ECommerce.Application.DTOs.Cari;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Authorize]
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
        // cari ekler
        [HttpPost]
        public async Task<IActionResult> Create(CariCreateDto dto)
        {
            await _cariService.AddAsync(dto);
            return Ok();
        }
        //cari günceller
        [HttpPut]
        public async Task<IActionResult> Update(CariUpdateDto dto)
        {
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
