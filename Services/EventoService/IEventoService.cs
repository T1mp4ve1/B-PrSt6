using Evento.Models.DTO.EventDTO;

namespace Evento.Services.EventoService
{
    public interface IEventoService
    {
        Task<EventoDto> CreateAsync(CreateEventoDto eventModel);
        Task<List<EventoDto>> GetAllAsync();
        Task<EventoDto> GetByIdAsync(int id);
        Task<EventoDto?> UpdateAsync(int id, CreateEventoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
