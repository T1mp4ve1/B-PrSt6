using Evento.Models.DTO.AatistaDTO;
using Evento.Services.ArtistaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistiController : ControllerBase
    {
        private readonly IArtistaService _service;
        public ArtistiController(IArtistaService service) => _service = service;

        [HttpPost]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Create([FromBody] CreateArtistaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ArtistaId }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                var exists = await _service.GetByIdAsync(id);
                if (exists == null) return NotFound();
                return BadRequest("Impossibile eliminare l'artista: esistono eventi associati.");
            }
            return NoContent();
        }
    }
}
