using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Evento.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cognome { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
    }
}