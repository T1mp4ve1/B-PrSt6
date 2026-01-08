using System.ComponentModel.DataAnnotations;

namespace Evento.Models
{
    public class EventoModel
    {
        [Key]
        [Required]
        public int EventoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Titolo { get; set; }

        [Required]
        public DateTime DataOra { get; set; }

        [Required]
        [MaxLength(50)]
        public string Luogo { get; set; }

        [Required]
        public int ArtistaId { get; set; }
        public Artista Artista { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
