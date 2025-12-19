using Evento.Data;
using Evento.Models;
using Evento.Models.DTO.BigliettoDTO;
using Microsoft.EntityFrameworkCore;

namespace Evento.Services.BigliettoService
{
    public class BigliettoService : IBigliettoService
    {
        private readonly AppDbContext _db;
        public BigliettoService(AppDbContext db) => _db = db;

        public async Task<BigliettoDto> CreateAsync(CreateBigliettoDto dto, string userId)
        {
            var evento = await _db.Eventi.FindAsync(dto.EventoId);
            if (evento == null) return null;

            var biglietto = new Biglietto
            {
                EventoId = dto.EventoId,
                DataOra = DateTime.UtcNow,
                UserId = userId
            };

            _db.Biglietti.Add(biglietto);
            await _db.SaveChangesAsync();

            return new BigliettoDto
            {
                BigliettoId = biglietto.BigliettoId,
                DataOra = biglietto.DataOra,
                EventoId = biglietto.EventoId,
                EventoTitolo = evento.Titolo,
                UserId = biglietto.UserId
            };
        }

        public async Task<List<BigliettoDto>> GetAllAsync()
        {
            return await _db.Biglietti
                .Include(b => b.Evento)
                .Select(b => new BigliettoDto
                {
                    BigliettoId = b.BigliettoId,
                    DataOra = b.DataOra,
                    EventoId = b.EventoId,
                    EventoTitolo = b.Evento.Titolo,
                    UserId = b.UserId
                }).ToListAsync();
        }

        public async Task<BigliettoDto> GetByIdAsync(int id)
        {
            var b = await _db.Biglietti.Include(x => x.Evento).FirstOrDefaultAsync(x => x.BigliettoId == id);
            if (b == null) return null;
            return new BigliettoDto
            {
                BigliettoId = b.BigliettoId,
                DataOra = b.DataOra,
                EventoId = b.EventoId,
                EventoTitolo = b.Evento?.Titolo,
                UserId = b.UserId
            };
        }
    }
}
