using AspNetCoreHero.ToastNotification.Abstractions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ApiController
    {
        private readonly IAirportService _airportService;
        public AirportsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<AirportDTO>> response = await _airportService.GetPaginatedListAsync(request);
            return ControllerBasicValidation(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            Response<AirportDTO> response = await _airportService.GetOneAsync(id);
            return ControllerBasicValidation(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AirportDTO dto)
        {
            Response<AirportDTO> response = await _airportService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] AirportDTO dto)
        {
            // Asegurar que el id de la ruta y el dto coincidan (o asignar)
            dto.Id = id;
            Response<AirportDTO> response = await _airportService.UpdateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _airportService.DeleteAsync(id);
            return ControllerBasicValidation(response);
        }
    }
}