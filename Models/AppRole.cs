using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Evento.Models
{
    public class AppRole : IdentityRole
    {
        [Required]
        [MaxLength(300)]
        public string? DescriptionRole { get; set; }
    }
}