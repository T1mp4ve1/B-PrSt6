using Evento.Models.DTO.AatistaDTO;

namespace Evento.Services.ArtistaService
{
    public interface IArtistaService
    {
        Task<ArtistaDto> CreateAsync(CreateArtistaDto artistaModel);
        Task<List<ArtistaDto>> GetAllAsync();
        Task<ArtistaDto?> GetByIdAsync(int id);
        Task<ArtistaDto?> UpdateAsync(int id, CreateArtistaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
