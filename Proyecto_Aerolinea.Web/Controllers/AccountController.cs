using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;

        public AccountController(DataContext context, IUserService userService, IMapper mapper, INotyfService notyfService)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Response<Microsoft.AspNetCore.Identity.SignInResult> result = await _userService.LoginAsync(dto);

                if (result.Succeed)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectas");
            }

            return View(dto);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(AccountUserDTO dto)
        {
            if (ModelState.IsValid)
            {
                Response<Microsoft.AspNetCore.Identity.IdentityResult> result = await _userService.SignupAsync(dto);

                if (result.Succeed)
                {
                    _notyfService.Success(result.Message);
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    _notyfService.Error(result.Message);
                }
            }

            return View(dto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            User user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            if (user is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AccountUserDTO>(user));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AccountUserDTO dto)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                Response<AccountUserDTO> result = await _userService.UpdateUserAsync(dto);

                if (result.Succeed)
                {
                    _notyfService.Success(result.Message);
                }
                else
                {
                    _notyfService.Error(result.Message);
                }

                return RedirectToAction("Index", "Home");
            }

            _notyfService.Error("Debe ajustar lo errores de validación");
            return View(dto);
        }
    }
}
