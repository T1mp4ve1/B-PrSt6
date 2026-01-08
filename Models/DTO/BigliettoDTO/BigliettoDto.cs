namespace Evento.Models.DTO.BigliettoDTO
{
    public class BigliettoDto
    {
        public int BigliettoId { get; set; }
        public DateTime DataOra { get; set; }
        public int EventoId { get; set; }
        public string EventoTitolo { get; set; }
        public string UserId { get; set; }

    }
}
