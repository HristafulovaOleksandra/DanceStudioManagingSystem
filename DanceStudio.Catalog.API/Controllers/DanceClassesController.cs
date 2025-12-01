using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Bll.Helpers;
using DanceStudio.Catalog.Bll.Services;
using DanceStudio.Catalog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DanceClassesController : ControllerBase
    {
        private readonly IDanceClassService _service;

        public DanceClassesController(IDanceClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<DanceClassDTO>>> GetAllClasses([FromQuery] DanceClassSpecParams specParams)
        {
            var result = await _service.GetAllClassesAsync(specParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _service.GetClassByIdAsync(id));
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanceClassCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateClassAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DanceClassCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _service.UpdateClassAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteClassAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}