using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragranceTypeController : ControllerBase
    {
        private readonly IFragranceTypeService _service;

        public FragranceTypeController(IFragranceTypeService service)
        {
            _service = service;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fragranceTypes = await _service.GetAllAsync();
            return Ok(fragranceTypes);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fragranceType = await _service.GetByIdAsync(id);
            return Ok(fragranceType);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create(FragranceTypeDto dto)
        {
            var created = await _service.CreateWithValidationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}