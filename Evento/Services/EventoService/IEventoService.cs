using Evento.Models;

namespace Evento.Services.EventoService
{
    public interface IEventoService
    {
        Task CreateAsync(EventoModel model);
        Task<EventoModel> GetAllAsync();
    }
}
