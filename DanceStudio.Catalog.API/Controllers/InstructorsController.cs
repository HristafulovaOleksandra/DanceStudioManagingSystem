using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Domain.Interfaces;
using DanceStudio.Catalog.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _instructorService.GetAllInstructorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _instructorService.GetInstructorByIdAsync(id));
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpGet("with-classes")]
        public async Task<IActionResult> GetWithClasses()
        {
            return Ok(await _instructorService.GetInstructorsWithClassesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstructorCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _instructorService.CreateInstructorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] InstructorCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _instructorService.UpdateInstructorAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _instructorService.DeleteInstructorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}