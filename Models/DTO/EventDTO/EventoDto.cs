namespace Evento.Models.DTO.EventDTO
{
    public class EventoDto
    {
        public int EventoId { get; set; }
        public string Titolo { get; set; }
        public DateTime DataOra { get; set; }
        public string Luogo { get; set; }
        public int ArtistaId { get; set; }
        public string ArtistaNome { get; set; }

    }
}
