using Evento.Data;
using Evento.Models;
using Evento.Models.DTO.EventDTO;
using Microsoft.EntityFrameworkCore;

namespace Evento.Services.EventoService
{
    public class EventoService : IEventoService
    {
        private readonly AppDbContext _db;
        public EventoService(AppDbContext db) => _db = db;

        public async Task<EventoDto> CreateAsync(CreateEventoDto dto)
        {
            var evento = new EventoModel
            {
                Titolo = dto.Titolo,
                DataOra = dto.DataOra,
                Luogo = dto.Luogo,
                ArtistaId = dto.ArtistaId
            };

            _db.Eventi.Add(evento);
            await _db.SaveChangesAsync();

            var artista = await _db.Artisti.FindAsync(evento.ArtistaId);

            return new EventoDto
            {
                EventoId = evento.EventoId,
                Titolo = evento.Titolo,
                DataOra = evento.DataOra,
                Luogo = evento.Luogo,
                ArtistaId = evento.ArtistaId,
                ArtistaNome = artista?.ArtistaNome
            };
        }

        public async Task<List<EventoDto>> GetAllAsync()
        {
            return await _db.Eventi
                .Include(e => e.Artista)
                .Select(e => new EventoDto
                {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    DataOra = e.DataOra,
                    Luogo = e.Luogo,
                    ArtistaId = e.ArtistaId,
                    ArtistaNome = e.Artista.ArtistaNome
                }).ToListAsync();
        }

        public async Task<EventoDto> GetByIdAsync(int id)
        {
            var e = await _db.Eventi.Include(x => x.Artista).FirstOrDefaultAsync(x => x.EventoId == id);
            if (e == null) return null;
            return new EventoDto
            {
                EventoId = e.EventoId,
                Titolo = e.Titolo,
                DataOra = e.DataOra,
                Luogo = e.Luogo,
                ArtistaId = e.ArtistaId,
                ArtistaNome = e.Artista?.ArtistaNome
            };
        }

        public async Task<EventoDto?> UpdateAsync(int id, CreateEventoDto dto)
        {
            var evento = await _db.Eventi.FindAsync(id);
            if (evento == null) return null;

            var artista = await _db.Artisti.FindAsync(dto.ArtistaId);
            if (artista == null) throw new InvalidOperationException("Artista non trovato");

            evento.Titolo = dto.Titolo;
            evento.DataOra = dto.DataOra;
            evento.Luogo = dto.Luogo;
            evento.ArtistaId = dto.ArtistaId;

            _db.Eventi.Update(evento);
            await _db.SaveChangesAsync();

            return new EventoDto
            {
                EventoId = evento.EventoId,
                Titolo = evento.Titolo,
                DataOra = evento.DataOra,
                Luogo = evento.Luogo,
                ArtistaId = evento.ArtistaId,
                ArtistaNome = artista.ArtistaNome
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _db.Eventi
                .Include(e => e.Biglietti)
                .FirstOrDefaultAsync(e => e.EventoId == id);

            if (evento == null) return false;

            using var tx = await _db.Database.BeginTransactionAsync();

            if (evento.Biglietti != null && evento.Biglietti.Any())
            {
                _db.Biglietti.RemoveRange(evento.Biglietti);
            }

            _db.Eventi.Remove(evento);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }
    }
}
