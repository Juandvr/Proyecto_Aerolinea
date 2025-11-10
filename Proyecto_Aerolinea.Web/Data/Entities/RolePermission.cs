namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class RolePermission
    {
        public required Guid RoleId { get; set; }

        public ProjectRole Role { get; set; }

        public required Guid PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
