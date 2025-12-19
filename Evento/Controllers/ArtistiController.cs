using Evento.Models.DTO.AatistaDTO;
using Evento.Services.ArtistaService;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Amministratore")]
    public class ArtistiController : ControllerBase
    {
        private readonly IArtistaService _service;
        public ArtistiController(IArtistaService service) => _service = service;

        [HttpPost]
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

    }
}
