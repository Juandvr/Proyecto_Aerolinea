using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using System.Security.Claims;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, SignInManager<User> signInManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<IdentityResult>> AddUserAsync(User user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            return new Response<IdentityResult>
            {
                Result = result,
                Succeed = result.Succeeded
            };
        }

        public async Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token)
        {
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

            return new Response<IdentityResult>
            {
                Result = result,
                Succeed = result.Succeeded
            };
        }
        //
        public bool CurrentUserIsAuthenticaded()
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity is not null && user.Identity.IsAuthenticated;
        }

        //

        public async Task<User> GetUserByEmailasync(string email)
        {
            return await _context.Users.Include(u => u.ProjectRole)
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module)
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;

            Console.WriteLine("DEBUG: CurrentUserIsAuthorizedAsync invoked");

            if (httpUser is null || !httpUser.Identity!.IsAuthenticated)
            {
                Console.WriteLine("DEBUG: no httpUser or not authenticated");
                return false;
            }

            // Obtener user desde UserManager (funciona con claims principales)
            var identityUser = await _userManager.GetUserAsync(httpUser);
            Console.WriteLine($"DEBUG: user from UserManager: {(identityUser == null ? "null" : identityUser.Email + " / " + identityUser.Id)}");

            if (identityUser is null)
                return false;

            // Cargar ProjectRole por si no está incluido
            var user = await _context.Users
                .Include(u => u.ProjectRole)
                .FirstOrDefaultAsync(u => u.Id == identityUser.Id);

            if (user is null)
            {
                Console.WriteLine("DEBUG: user not found in DB by Id");
                return false;
            }

            Console.WriteLine($"DEBUG: DB user: {user.Email}, ProjectRoleId: {user.ProjectRoleId}, ProjectRole.Name: {user.ProjectRole?.Name}");

            if (user.ProjectRole?.Name == Env.SUPER_ADMIN_ROLE_NAME)
            {
                Console.WriteLine("DEBUG: is super admin -> authorized");
                return true;
            }

            bool has = await _context.Permissions
                .Include(p => p.RolePermissions)
                .AnyAsync(p => p.Module == module && p.Name == permission && p.RolePermissions.Any(rp => rp.RoleId == user.ProjectRoleId));

            Console.WriteLine($"DEBUG: permission check for {module}/{permission} => {has}");
            return has;
        }



        //



        public async Task<Response<string>> GenerateConfirmationTokenAsync(User user)
        {
            string result = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Response<string>.Success(result);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.ProjectRole).
                FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Response<SignInResult>> LoginAsync(LoginDTO dto)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            return new Response<SignInResult>
            {
                Result = result,
                Succeed = result.Succeeded
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // Signup de un usuario
        public async Task<Response<IdentityResult>> SignupAsync(AccountUserDTO dto)
        {
            
            const string defaultRoleName = "User";

            var role = await _context.ProjectRoles.FirstOrDefaultAsync(r => r.Name == defaultRoleName);

            if (role == null)
            {
                role = new ProjectRole { Id = Guid.NewGuid(), Name = defaultRoleName };
                await _context.ProjectRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.Email,
                Email = dto.Email,
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Photo = dto.Photo,
                EmailConfirmed = true,
                ProjectRoleId = role.Id 
            };

            return await AddUserAsync(user, dto.Password);
        }


        public async Task<Response<AccountUserDTO>> UpdateUserAsync(AccountUserDTO dto)
        {
            try
            {
                User user = await GetUserAsync(dto.Id);
                user.PhoneNumber = dto.PhoneNumber;
                user.Document = dto.Document;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Photo = dto.Photo;

                _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return Response<AccountUserDTO>.Success(dto, "Datos actualizados con éxito");
            }
            catch (Exception ex)
            {
                return Response<AccountUserDTO>.Failure(ex);
            }
        }

        private async Task<User> GetUserAsync(string? id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}