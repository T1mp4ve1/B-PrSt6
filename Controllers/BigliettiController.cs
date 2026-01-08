using Evento.Models.DTO.BigliettoDTO;
using Evento.Services.BigliettoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigliettiController : ControllerBase
    {
        private readonly IBigliettoService _service;
        public BigliettiController(IBigliettoService service) => _service = service;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBigliettoDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var created = await _service.CreateAsync(dto, userId);
            if (created == null) return BadRequest("Evento non trovato");
            return CreatedAtAction(nameof(GetById), new { id = created.BigliettoId }, created);
        }

        [HttpGet]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _service.GetByIdAsync(id);
            if (ticket == null) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Amministratore");
            if (!isAdmin && ticket.UserId != userId) return Forbid();

            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Amministratore");

            var success = await _service.DeleteAsync(id, userId, isAdmin);
            if (!success)
            {
                var ticket = await _service.GetByIdAsync(id);
                if (ticket == null) return NotFound();
                return Forbid();
            }

            return NoContent();
        }
    }
}
