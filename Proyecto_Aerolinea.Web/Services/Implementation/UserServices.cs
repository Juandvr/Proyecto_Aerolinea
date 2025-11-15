using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using System.Security.Claims;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;
        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context, IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccesor = httpContextAccesor;
        }
        public async Task<Response<IdentityResult>> AddUserAsync(User user, string password)
        {
            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                return new Response<IdentityResult>
                {
                    Succeed = result.Succeeded,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return Response<IdentityResult>.Failure(ex);
            }

        }

        public async Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token)
        {
            try
            {
                IdentityResult confir = await _userManager.ConfirmEmailAsync(user, token);

                return new Response<IdentityResult>
                {
                    Succeed = confir.Succeeded,
                    Result = confir
                };
            }
            catch (Exception ex)
            {
                return Response<IdentityResult>.Failure(ex);
            }
        }

        public bool CurrentIsAuthenticaded()
        {
            ClaimsPrincipal? user = _httpContextAccesor.HttpContext.User;
            return user.Identity != null && user.Identity.IsAuthenticated;
        }

        public async Task<Response<string>> GenerateConfirmationTokenAsync(User user)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Response<string>.Success(token);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<Response<SignInResult>> LoginAsync(LoginDTO dto)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            return Response<SignInResult>.Success(result);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Response<IdentityResult>> SignupAsync(UserDTO dto)
        {
            const string defaultName = "User";

            var role = await _context.ProjectRoles.FirstOrDefaultAsync(s => s.Name == defaultName);
            if (role == null)
            {
                role.Id = Guid.NewGuid();
                role.Name = defaultName;
                await _context.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Photo = dto.Photo,
                ProjectRoleId = role.Id,
                EmailConfirmed = true,
                UserName = dto.Email
            };
            return await AddUserAsync(user, dto.Password);
        }

        public async Task<Response<UserDTO>> UpdateUserAsync(UserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
