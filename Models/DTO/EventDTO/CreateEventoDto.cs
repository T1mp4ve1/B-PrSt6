namespace Evento.Models.DTO.EventDTO
{
    public class CreateEventoDto
    {
        public string Titolo { get; set; }
        public DateTime DataOra { get; set; }
        public string Luogo { get; set; }
        public int ArtistaId { get; set; }

    }
}