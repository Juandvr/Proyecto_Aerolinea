using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.DTOs;


namespace Proyecto_Aerolinea.Web.Services.Abtractions
{
    public interface IRolesService
    {
        public Task<Response<ProjectRoleDTO>> CreateAsync(ProjectRoleDTO dto);
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<ProjectRoleDTO>> EditAsync(ProjectRoleDTO dto);
        public Task<Response<ProjectRoleDTO>> GetOneAsync(Guid id);
        public Task<Response<PaginationResponse<ProjectRoleDTO>>> GetPaginatedListAsync(PaginationRequest request);
        public Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync();
    }
}