using System.ComponentModel.DataAnnotations;

namespace Evento.Models
{
    public class Artista
    {
        [Key]
        [Required]
        public int ArtistaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ArtistaNome { get; set; }

        [Required]
        [MaxLength(20)]
        public string Genere { get; set; }

        [Required]
        [MaxLength(500)]
        public string Biografia { get; set; }

        public ICollection<EventoModel> Eventi { get; set; }
    }
}