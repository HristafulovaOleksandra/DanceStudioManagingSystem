using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Domain.Interfaces;
using DanceStudio.Catalog.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DanceClassDetailsController : ControllerBase
    {
        private readonly IDanceClassDetailService _service;

        public DanceClassDetailsController(IDanceClassDetailService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _service.GetDetailByIdAsync(id));
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpGet("{id}/with-class")]
        public async Task<IActionResult> GetWithClass(long id)
        {
            try
            {
                return Ok(await _service.GetDetailWithClassAsync(id));
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanceClassDetailCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.CreateDetailAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DanceClassDetailCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _service.UpdateDetailAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteDetailAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}