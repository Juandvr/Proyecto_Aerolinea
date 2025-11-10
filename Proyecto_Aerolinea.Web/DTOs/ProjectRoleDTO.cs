using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class ProjectRoleDTO : IId
    {
        public Guid Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        public List<PermissionsForRoleDTO>? Permissions { get; set; }

        public string? PermissionIds { get; set; }
    }

    public class PermissionsForRoleDTO : PermissionDTO
    {
        public bool Selected { get; set; }
    }
}