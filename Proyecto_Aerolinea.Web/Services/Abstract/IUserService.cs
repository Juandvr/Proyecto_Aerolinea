using Microsoft.AspNetCore.Identity;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IUserService
    {
        public Task<Response<IdentityResult>> AddUserAsync(User user, string password);
        public Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token);
        public Task<Response<string>> GenerateConfirmationTokenAsync(User user);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<Response<SignInResult>> LoginAsync(LoginDTO dto);
        public Task<Response<Microsoft.AspNetCore.Identity.IdentityResult>> SignupAsync(AccountUserDTO dto);
        public Task LogoutAsync();
        public Task<Response<AccountUserDTO>> UpdateUserAsync(AccountUserDTO dto);

        public bool CurrentUserIsAuthenticaded();
        public Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module);
    }
}
