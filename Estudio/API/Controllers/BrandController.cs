using Estudio.Application.Interface;
using Estudio.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Estudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _service.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandDto dto)
        {
            var created = await _service.CreateWithValidationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    } 
}