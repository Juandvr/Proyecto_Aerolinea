namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class RolePermission
    {
        public Guid ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}