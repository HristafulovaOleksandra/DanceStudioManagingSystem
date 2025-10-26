using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Booking.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // POST /api/clients
        [HttpPost]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            

            var newClient = await _clientService.CreateAsync(dto);

            
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }

        // GET /api/clients/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientById(long id)
        {
            
            var client = await _clientService.GetByIdAsync(id);
            return Ok(client);
        }

        
    }
}