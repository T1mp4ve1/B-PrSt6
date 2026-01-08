using Evento.Data;
using Evento.Models;
using Evento.Models.DTO.AatistaDTO;
using Microsoft.EntityFrameworkCore;

namespace Evento.Services.ArtistaService
{
    public class ArtistaService : IArtistaService
    {
        private readonly AppDbContext _db;
        public ArtistaService(AppDbContext db) => _db = db;

        public async Task<ArtistaDto> CreateAsync(CreateArtistaDto dto)
        {
            var entity = new Artista
            {
                ArtistaNome = dto.ArtistaNome,
                Genere = dto.Genere,
                Biografia = dto.Biografia
            };

            _db.Artisti.Add(entity);
            await _db.SaveChangesAsync();

            return new ArtistaDto
            {
                ArtistaId = entity.ArtistaId,
                ArtistaNome = entity.ArtistaNome,
                Genere = entity.Genere,
                Biografia = entity.Biografia
            };
        }

        public async Task<List<ArtistaDto>> GetAllAsync()
        {
            return await _db.Artisti
                .Select(a => new ArtistaDto
                {
                    ArtistaId = a.ArtistaId,
                    ArtistaNome = a.ArtistaNome,
                    Genere = a.Genere,
                    Biografia = a.Biografia
                }).ToListAsync();
        }

        public async Task<ArtistaDto> GetByIdAsync(int id)
        {
            var a = await _db.Artisti.FindAsync(id);
            if (a == null) return null;

            return new ArtistaDto
            {
                ArtistaId = a.ArtistaId,
                ArtistaNome = a.ArtistaNome,
                Genere = a.Genere,
                Biografia = a.Biografia
            };
        }

        public async Task<ArtistaDto?> UpdateAsync(int id, CreateArtistaDto dto)
        {
            var artista = await _db.Artisti.FindAsync(id);
            if (artista == null) return null;

            artista.ArtistaNome = dto.ArtistaNome;
            artista.Genere = dto.Genere;
            artista.Biografia = dto.Biografia;

            _db.Artisti.Update(artista);
            await _db.SaveChangesAsync();

            return new ArtistaDto
            {
                ArtistaId = artista.ArtistaId,
                ArtistaNome = artista.ArtistaNome,
                Genere = artista.Genere,
                Biografia = artista.Biografia
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var artista = await _db.Artisti.FirstOrDefaultAsync(a => a.ArtistaId == id);

            if (artista == null) return false;

            _db.Artisti.Remove(artista);
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
