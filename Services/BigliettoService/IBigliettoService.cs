using Evento.Models.DTO.BigliettoDTO;

namespace Evento.Services.BigliettoService
{
    public interface IBigliettoService
    {
        Task<BigliettoDto> CreateAsync(CreateBigliettoDto biglietto, string userId);
        Task<List<BigliettoDto>> GetAllAsync();
        Task<BigliettoDto> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id, string? userId = null, bool isAdmin = false);
    }
}
