using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(DataContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
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

        public async Task<Response<Microsoft.AspNetCore.Identity.IdentityResult>> SignupAsync(AccountUserDTO dto)
        {
            bool roleExists = await _context.ProjectRoles.AnyAsync(r => r.Name == "Admin");

            if (!roleExists)
            {
                ProjectRole role = new ProjectRole { Id = Guid.NewGuid(), Name = "Admin" };
                await _context.ProjectRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            ProjectRole adminRole = await _context.ProjectRoles.FirstOrDefaultAsync(r => r.Name == "Admin");

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
                ProjectRoleId = adminRole!.Id
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