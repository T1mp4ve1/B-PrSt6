using System.ComponentModel.DataAnnotations;

namespace Evento.Models
{
    public class Biglietto
    {
        [Key]
        [Required]
        public int BigliettoId { get; set; }

        [Required]
        public DateTime DataOra { get; set; }

        [Required]
        public int EventoId { get; set; }
        public EventoModel Evento { get; set; }

        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}