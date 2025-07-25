using Estudio.API.DTO;
using Estudio.Application.Interface;
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

            var resultDto = new BrandDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, resultDto);
        }
    } 
}


//[HttpPatch("{id:guid}/discount")]
//public async Task<IActionResult> ApplyDiscount(Guid id, [FromQuery] decimal percent)
//{
//    var updated = await _service.ApplyDiscountAsync(id, percent);
//    return updated == null ? NotFound() : Ok(updated);
//}

//[HttpGet("category/{category}")]
//public async Task<IActionResult> GetByCategory(string category)
//{
//    var products = await _service.GetByCategoryAsync(category);
//    return Ok(products);
//}