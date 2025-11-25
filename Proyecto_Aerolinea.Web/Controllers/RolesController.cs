using AspNetCoreHero.ToastNotification.Abstractions;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abtractions;
using System.Runtime.CompilerServices;
using Proyecto_Aerolinea.Web.Core.Attributes;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;
        private readonly INotyfService _notyfService;

        public RolesController(IRolesService rolesService, INotyfService notyfService)
        {
            _rolesService = rolesService;
            _notyfService = notyfService;
        }

        // Index - List Roles

        [HttpGet]
        [CustomAuthorize(permission: "showRoles", module: "Roles")]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<ProjectRoleDTO>> response = await _rolesService.GetPaginatedListAsync(request);

            if (!response.Succeed)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        // Delete Role

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _rolesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        // Create Role

        [HttpGet]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            Response<List<PermissionsForRoleDTO>> permissionsResponse = await _rolesService.GetPermissionsAsync();

            if (!permissionsResponse.Succeed)
            {
                _notyfService.Error(permissionsResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            ProjectRoleDTO dto = new ProjectRoleDTO
            {
                Permissions = permissionsResponse.Result
            };

            return View(dto);
        }

        [HttpPost]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(ProjectRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");

                Response<List<PermissionsForRoleDTO>> permissionsResponse = await _rolesService.GetPermissionsAsync();

                if (!permissionsResponse.Succeed)
                {
                    _notyfService.Error(permissionsResponse.Message);
                    return RedirectToAction(nameof(Index));
                }

                dto.Permissions = permissionsResponse.Result;

                return View(dto);
            }

            Response<ProjectRoleDTO> createResponse = await _rolesService.CreateAsync(dto);
            if (createResponse.Succeed)
            {
                _notyfService.Success(createResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notyfService.Error(createResponse.Message);

            Response<List<PermissionsForRoleDTO>> permissionsResponse2 = await _rolesService.GetPermissionsAsync();

            if (!permissionsResponse2.Succeed)
            {
                _notyfService.Error(permissionsResponse2.Message);
                return RedirectToAction(nameof(Index));
            }

            dto.Permissions = permissionsResponse2.Result;
            return View(dto);
        }


        // Update Role

        [HttpGet]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Response<ProjectRoleDTO> response = await _rolesService.GetOneAsync(id);

            if (!response.Succeed)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        [HttpPost]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(ProjectRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");

                Response<List<PermissionsForRoleDTO>> permissionsResponse = await _rolesService.GetPermissionsAsync();

                if (!permissionsResponse.Succeed)
                {
                    _notyfService.Error(permissionsResponse.Message);
                    return RedirectToAction(nameof(Index));
                }

                dto.Permissions = permissionsResponse.Result;

                return View(dto);
            }

            Response<ProjectRoleDTO> updateResponse = await _rolesService.EditAsync(dto);
            if (updateResponse.Succeed)
            {
                _notyfService.Success(updateResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notyfService.Error(updateResponse.Message);

            Response<List<PermissionsForRoleDTO>> permissionsResponse2 = await _rolesService.GetPermissionsAsync();

            if (!permissionsResponse2.Succeed)
            {
                _notyfService.Error(permissionsResponse2.Message);
                return RedirectToAction(nameof(Index));
            }

            dto.Permissions = permissionsResponse2.Result;
            return View(dto);
        }
    }
}