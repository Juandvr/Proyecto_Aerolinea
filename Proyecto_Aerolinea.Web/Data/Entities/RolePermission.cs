namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class RolePermission
    {
        public required Guid RoleId { get; set; }

        public required ProjectRole Role { get; set; }

        public required Guid PermissionId { get; set; }

        public required Permission Permission { get; set; }
    }
}
