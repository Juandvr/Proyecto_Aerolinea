using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Abtractions;

namespace Proyecto_Aerolinea.Web.Services.Implementations
{
    public class RolesService : CustomQueryableOperationsService, IRolesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RolesService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ Crear un rol y asignar permisos
        public async Task<Response<ProjectRoleDTO>> CreateAsync(ProjectRoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ProjectRole role = _mapper.Map<ProjectRole>(dto);
                    role.Id = Guid.NewGuid();

                    await _context.ProjectRoles.AddAsync(role);
                    await _context.SaveChangesAsync();

                    // Deserializar los permisos seleccionados (si vienen en JSON)
                    List<Guid> permissionIds = new();

                    if (!string.IsNullOrEmpty(dto.PermissionIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionIds);
                    }

                    foreach (Guid permissionId in permissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionId
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Response<ProjectRoleDTO>.Success(dto, "Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Response<ProjectRoleDTO>.Failure(ex);
                }
            }
        }

        // ✅ Eliminar un rol
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<ProjectRole>(id);
        }

        // ✅ Editar un rol y actualizar sus permisos
        public async Task<Response<ProjectRoleDTO>> EditAsync(ProjectRoleDTO dto)
        {
            try
            {
                if (dto.Name == Env.SUPER_ADMIN_ROLE_NAME)
                {
                    return Response<ProjectRoleDTO>.Failure($"El rol '{Env.SUPER_ADMIN_ROLE_NAME}' no puede ser editado");
                }

                ProjectRole role = _mapper.Map<ProjectRole>(dto);
                _context.ProjectRoles.Update(role);

                List<Guid> permissionIds = new();

                if (!string.IsNullOrEmpty(dto.PermissionIds))
                {
                    permissionIds = JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionIds);
                }

                // Eliminar permisos antiguos
                var oldPermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == dto.Id)
                    .ToListAsync();

                _context.RolePermissions.RemoveRange(oldPermissions);

                // Agregar nuevos
                foreach (Guid permissionId in permissionIds)
                {
                    var newRolePermission = new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permissionId
                    };
                    await _context.RolePermissions.AddAsync(newRolePermission);
                }

                await _context.SaveChangesAsync();

                return Response<ProjectRoleDTO>.Success(dto, "Rol actualizado con éxito");
            }
            catch (Exception ex)
            {
                return Response<ProjectRoleDTO>.Failure(ex);
            }
        }

        // ✅ Obtener un rol con sus permisos
        public async Task<Response<ProjectRoleDTO>> GetOneAsync(Guid id)
        {
            Response<ProjectRoleDTO> response = await GetOneAsync<ProjectRole, ProjectRoleDTO>(id);

            if (!response.Succeed)
                return response;

            var dto = response.Result;

            var permissions = await _context.Permissions
                .Select(p => new PermissionsForRoleDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    Module = p.Module,
                    Selected = _context.RolePermissions
                        .Any(rp => rp.PermissionId == p.Id && rp.RoleId == dto.Id)
                })
                .ToListAsync();

            dto.Permissions = permissions;

            return Response<ProjectRoleDTO>.Success(dto, "Rol obtenido con éxito");
        }

        // ✅ Obtener lista paginada de roles
        public async Task<Response<PaginationResponse<ProjectRoleDTO>>> GetPaginatedListAsync(PaginationRequest request)
        {
            Console.WriteLine("🟡 Entrando a GetPaginatedListAsync...");

            IQueryable<ProjectRole> query = _context.ProjectRoles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(r => r.Name.ToLower().Contains(request.Filter.ToLower()));
            }

            var result = await GetPaginationAsync<ProjectRole, ProjectRoleDTO>(request, query);

            Console.WriteLine($"🟢 Result Succeed: {result.Succeed}, Message: {result.Message}");

            return result;
        }


        // ✅ Obtener lista de permisos
        public async Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync()
        {
            Response<List<PermissionDTO>> permissionsResponse =
                await GetCompleteListAsync<Permission, PermissionDTO>();

            if (!permissionsResponse.Succeed)
            {
                return Response<List<PermissionsForRoleDTO>>.Failure(permissionsResponse.Message);
            }

            var dto = permissionsResponse.Result.Select(p => new PermissionsForRoleDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Selected = false
            }).ToList();

            return Response<List<PermissionsForRoleDTO>>.Success(dto);
        }
    }
}
