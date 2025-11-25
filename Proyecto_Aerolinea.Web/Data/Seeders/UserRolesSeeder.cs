using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Data.Seeders
{
    public class UserRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUserService _usersService;
        private readonly UserManager<User> _userManager;
        private const string CONTENT_MANAGER_ROLE_NAME = "Gestor de contenido";
        private const string BASIC_ROLE_NAME = "Basic";
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesSeeder(DataContext context, IUserService usersService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _usersService = usersService;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task SeedAsync()
        {
            await CheckRolesAsync();
            await CheckUsersAsync();
        }

        private async Task CheckRolesAsync()
        {
            await AdminRoleAsync();
            await BasicRoleAsync();
            await ContentManagerRoleAsync();
        }

        private async Task CheckUsersAsync()
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "manuel@yopmail.com");

            if (user is null)
            {
                ProjectRole adminRole = await _context.ProjectRoles
                    .FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                user = new User
                {
                    Email = "manuel@yopmail.com",
                    FirstName = "Manuel",
                    LastName = "Domínguez",
                    PhoneNumber = "3000000000",
                    UserName = "manuel@yopmail.com",
                    Document = "1111",
                    ProjectRoleId = adminRole!.Id
                };

                await _usersService.AddUserAsync(user, "1234");

                // 🔹 Asignamos el rol de Identity al usuario
                await _userManager.AddToRoleAsync(user, Env.SUPER_ADMIN_ROLE_NAME);

                string token = (await _usersService.GenerateConfirmationTokenAsync(user)).Result;
                await _usersService.ConfirmUserAsync(user, token);
            }
        }



        private async Task AdminRoleAsync()
        {
            // ProjectRole (tu tabla de negocio)
            if (!await _context.ProjectRoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME))
            {
                ProjectRole role = new ProjectRole { Id = Guid.NewGuid(), Name = Env.SUPER_ADMIN_ROLE_NAME };
                await _context.ProjectRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            // Identity Role (tabla AspNetRoles)
            if (!await _roleManager.RoleExistsAsync(Env.SUPER_ADMIN_ROLE_NAME))
            {
                await _roleManager.CreateAsync(new IdentityRole(Env.SUPER_ADMIN_ROLE_NAME));
            }
        }

        private async Task BasicRoleAsync()
        {
            if (!await _context.ProjectRoles.AnyAsync(r => r.Name == BASIC_ROLE_NAME))
            {
                ProjectRole role = new ProjectRole { Id = Guid.NewGuid(), Name = BASIC_ROLE_NAME };
                await _context.ProjectRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            // 👇 también crear el rol en Identity
            if (!await _roleManager.RoleExistsAsync(BASIC_ROLE_NAME))
            {
                await _roleManager.CreateAsync(new IdentityRole(BASIC_ROLE_NAME));
            }
        }

        private async Task ContentManagerRoleAsync()
        {
            bool exists = await _context.ProjectRoles.AnyAsync(r => r.Name == CONTENT_MANAGER_ROLE_NAME);

            if (!exists)
            {
                // Crear en ProjectRoles
                ProjectRole role = new ProjectRole { Id = Guid.NewGuid(), Name = CONTENT_MANAGER_ROLE_NAME };
                await _context.ProjectRoles.AddAsync(role);
                await _context.SaveChangesAsync();

                // Crear también en Identity
                if (!await _roleManager.RoleExistsAsync(CONTENT_MANAGER_ROLE_NAME))
                {
                    await _roleManager.CreateAsync(new IdentityRole(CONTENT_MANAGER_ROLE_NAME));
                }

                // Asociar permisos a este rol
                List<Permission> permissions = await _context.Permissions
                    .Where(p => p.Module == "Aeropuertos" || p.Module == "Aviones")
                    .ToListAsync();

                foreach (Permission permission in permissions)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id,
                        Role = role,
                        Permission = permission
                    };

                    await _context.RolePermissions.AddAsync(rolePermission);
                }

                await _context.SaveChangesAsync();
            }
        }


    }
}
