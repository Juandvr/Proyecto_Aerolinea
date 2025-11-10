using Microsoft.AspNetCore.Identity;
using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Required, StringLength(50)]
        public string Document { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public Guid ProjectRoleId { get; set; }

        public ProjectRole ProjectRole { get; set; } = null!;

        public string? Photo { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
