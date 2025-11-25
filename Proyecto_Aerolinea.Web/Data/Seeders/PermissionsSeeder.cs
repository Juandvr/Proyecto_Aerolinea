using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.Data;
using System.Data;

namespace Proyecto_Aerolinea.Web.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Permission> permissions = [.. Airports(), .. Airplanes(), .. Roles()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();

            // Asegurar que exista el rol "Admin"
            var adminRole = await _context.ProjectRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = new ProjectRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin"
                };
                await _context.ProjectRoles.AddAsync(adminRole);
                await _context.SaveChangesAsync();
            }

            var allPermissions = await _context.Permissions.ToListAsync();

            foreach (var permission in allPermissions)
            {
                bool alreadyLinked = await _context.RolePermissions
                    .AnyAsync(rp => rp.RoleId == adminRole.Id && rp.PermissionId == permission.Id);

                if (!alreadyLinked)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            await _context.SaveChangesAsync();

        }
        

        // -------------------------
        // MÓDULO: AEROPUERTOS
        // -------------------------
        private List<Permission> Airports() => new()
        {
            new Permission { Id = Guid.NewGuid(), Name = "showAirports", Description = "Ver Aeropuertos", Module = "Aeropuertos"},
            new Permission { Id = Guid.NewGuid(), Name = "createAirports", Description = "Crear Aeropuertos", Module = "Aeropuertos"},
            new Permission { Id = Guid.NewGuid(), Name = "updateAirports", Description = "Editar Aeropuertos", Module = "Aeropuertos"},
            new Permission { Id = Guid.NewGuid(), Name = "deleteAirports", Description = "Eliminar Aeropuertos", Module = "Aeropuertos"},
        };

        // -------------------------
        // MÓDULO: AVIONES
        // -------------------------
        private List<Permission> Airplanes() => new()
        {
            new Permission { Id = Guid.NewGuid(), Name = "showAirplanes", Description = "Ver Aviones", Module = "Aviones"},
            new Permission { Id = Guid.NewGuid(), Name = "createAirplanes", Description = "Crear Aviones", Module = "Aviones"},
            new Permission { Id = Guid.NewGuid(), Name = "updateAirplanes", Description = "Editar Aviones", Module = "Aviones"},
            new Permission { Id = Guid.NewGuid(), Name = "deleteAirplanes", Description = "Eliminar Aviones", Module = "Aviones"},
        };

        // -------------------------
        // MÓDULO: ROLES
        // -------------------------
        private List<Permission> Roles() => new()
        {
            new Permission { Id = Guid.NewGuid(), Name = "showRoles", Description = "Ver Roles", Module = "Roles"},
            new Permission { Id = Guid.NewGuid(), Name = "createRoles", Description = "Crear Roles", Module = "Roles"},
            new Permission { Id = Guid.NewGuid(), Name = "updateRoles", Description = "Editar Roles", Module = "Roles"},
            new Permission { Id = Guid.NewGuid(), Name = "deleteRoles", Description = "Eliminar Roles", Module = "Roles"},
        };

    }
}
