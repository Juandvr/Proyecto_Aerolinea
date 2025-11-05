using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class ProjectRole
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
