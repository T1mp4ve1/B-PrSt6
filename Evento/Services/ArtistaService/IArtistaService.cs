using Evento.Models;

namespace Evento.Services.ArtistaService
{
    public interface IArtistaService
    {
        Task CreateAsync(Artista artista);
        Task<Artista> GetAllAsync();
    }
}
