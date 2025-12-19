using Evento.Models;

namespace Evento.Services.BigliettoService
{
    public interface IBigliettoService
    {
        Task CreateAsync(Biglietto biglietto);
        Task<Biglietto> GetAllAsync();
    }
}
